using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class MaitreD
    {
        public Maybe<Reservation> TryAccept(bool tableAvailable, Reservation reservation)
        {
            if (!tableAvailable)
            {
                return new Maybe<Reservation>();
            }

            reservation.IsAccepted = true;
            return new Maybe<Reservation>(reservation);
        }
    }
}
