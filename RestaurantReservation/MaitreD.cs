using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class MaitreD
    {
        public int Capacity { get; }

        public MaitreD(int capacity)
        {
            Capacity = capacity;
        }

        public Reservation Accept(Reservation reservation)
        {
            reservation.IsAccepted = true;
            return reservation;
        }

        public Maybe<Reservation> CheckTableAvailability(Reservation[] reservations, Reservation reservation)
        {
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
            {
                return new Maybe<Reservation>();
            }

            return new Maybe<Reservation>(reservation);
        }

        public Maybe<Reservation> AskConfirmation(bool[] confirmations, Reservation reservation)
        {
            return confirmations.All(x => true)
                ? new Maybe<Reservation>()
                : new Maybe<Reservation>(reservation);
        }
    }
}
