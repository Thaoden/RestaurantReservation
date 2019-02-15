using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(IMaitreD maitreD)
        {
            MaitreD = maitreD;
        }

        public IMaitreD MaitreD { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            int? id = await MaitreD.TryAccept(reservation);
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