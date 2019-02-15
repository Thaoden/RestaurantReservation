using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public interface ITelephone
    {
        Task<bool> AskConfirmation(string phoneNumber);
    }
}
