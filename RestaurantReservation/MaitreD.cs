using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class MaitreD : IMaitreD
    {
        public int Capacity { get; }
        public ITableAvailabilityChecker TableAvailabilityChecker { get; }

        public MaitreD(int capacity, ITableAvailabilityChecker tableAvailabilityChecker)
        {
            Capacity = capacity;
            TableAvailabilityChecker = tableAvailabilityChecker;
        }

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
