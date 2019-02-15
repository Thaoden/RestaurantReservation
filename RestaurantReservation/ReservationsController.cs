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
            int? id = await MaitreD.TryAccept(tableAvailable, reservation);
            if (id == null)
                return new ContentResult
                {
                    Content = "Table unavailable",
                    StatusCode = StatusCodes.Status500InternalServerError
                };

            return Ok(id.Value);
        }
    }
}