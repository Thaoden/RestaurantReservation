using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class MaitreD
    {
        public Reservation Accept(Reservation reservation)
        {
            reservation.IsAccepted = true;
            return reservation;
        }
    }
}
