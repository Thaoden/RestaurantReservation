using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class ReservationsController : ControllerBase
    {
        private readonly MaitreD _maitreD;

        public ReservationsController(ITableAvailabilityChecker tableAvailabilityChecker)
        {
            _maitreD = new MaitreD();
            TableAvailabilityChecker = tableAvailabilityChecker;
        }

        public ITableAvailabilityChecker TableAvailabilityChecker { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            return (await TableAvailabilityChecker.CheckTableAvailability(reservation))
                .Match(
                    some: r => Ok(TableAvailabilityChecker.Create(_maitreD.Accept(r))),
                    none: new ContentResult { Content = "Table unavailable", StatusCode = StatusCodes.Status500InternalServerError } as ActionResult
                );
        }
    }
}