using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class ReservationsController : ControllerBase
    {
        private readonly MaitreD _maitreD;

        public ReservationsController(int capacity, IReservationsRepository repository, ITelephone telephone)
        {
            _maitreD = new MaitreD(capacity);
            Repository = repository;
            Telephone = telephone;
        }

        public IReservationsRepository Repository { get; }
        public ITelephone Telephone { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            Reservation[] currentReservations = await Repository.ReadReservations(reservation.Date);
            var confirmationCalls = currentReservations.Select(cr => Telephone.AskConfirmation(cr.Guest.PhoneNumber));
            var confirmations = await Task.WhenAll(confirmationCalls);

            return _maitreD.CheckTableAvailability(currentReservations, reservation)
                .Match(
                    some: r => new Maybe<Reservation>(r),
                    none: _maitreD.AskConfirmation(confirmations, reservation)
                )
                .Match(
                    some: r => Ok(Repository.Create(_maitreD.Accept(r))),
                    none: new ContentResult { Content = "Table unavailable", StatusCode = StatusCodes.Status500InternalServerError } as ActionResult
                );
        }
    }
}

