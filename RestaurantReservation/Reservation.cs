using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class Reservation
    {
        public DateTimeOffset Date { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsAccepted { get; set; }

        public Guest Guest { get; set; }
    }

    public class Guest
    {
        public string PhoneNumber { get; set; }
    }
}
