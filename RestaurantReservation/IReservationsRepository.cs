using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public interface IReservationsRepository
    {
        Task<Reservation[]> ReadReservations(DateTimeOffset date);
        Task<int> Create(Reservation reservation);
    }
}
