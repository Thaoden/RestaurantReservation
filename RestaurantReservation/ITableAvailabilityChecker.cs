using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public interface ITableAvailabilityChecker
    {
        Task<bool> CheckTableAvailability(Reservation reservation);
        Task<int?> Create(Reservation reservation);
    }
}
