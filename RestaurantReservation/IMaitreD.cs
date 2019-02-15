using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public interface IMaitreD
    {
        Maybe<Reservation> TryAccept(bool tableAvailable, Reservation reservation);
    }
}
