using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(IMaitreD maitreD, ITableAvailabilityChecker tableAvailabilityChecker)
        {
            MaitreD = maitreD;
            TableAvailabilityChecker = tableAvailabilityChecker;
        }

        public IMaitreD MaitreD { get; }
        public ITableAvailabilityChecker TableAvailabilityChecker { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            var tableAvailable = await TableAvailabilityChecker.CheckTableAvailability(reservation);

            var reservationMaybe = MaitreD.TryAccept(tableAvailable, reservation);

            return await reservationMaybe
                .Select(async r => await TableAvailabilityChecker.Create(r))
                .Match<Task<ActionResult>>(
                    none: Task.FromResult(new ContentResult { Content = "Table unavailable", StatusCode = StatusCodes.Status500InternalServerError } as ActionResult),
                    some: async id => Ok(await id)
                );
        }
    }
}