using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int?> TryAccept(bool tableAvailable, Reservation reservation)
        {
            if (tableAvailable)
            {
                reservation.IsAccepted = true;
                return await TableAvailabilityChecker.Create(reservation);
            }
            else
            {
                return null;
            }
        }
    }
}
