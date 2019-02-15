using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thaoden.RestaurantReservation
{
    public class TableAvailabilityChecker : ITableAvailabilityChecker
    {
        public int Capacity { get; }
        public IReservationsRepository Repository { get; }
        public ITelephone Telephone { get; }


        public TableAvailabilityChecker(int capacity, IReservationsRepository repository, ITelephone telephone)
        {
            Capacity = capacity;
            Repository = repository;
            Telephone = telephone;
        }

        public async Task<bool> CheckTableAvailability(Reservation reservation)
        {
            var reservations = await Repository.ReadReservations(reservation.Date);
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
            {
                foreach (var r in reservations)
                {
                    if (!(await Telephone.AskConfirmation(r.Guest.PhoneNumber)))
                    {
                        //some guest cannot make it for his reservation
                        return true;
                    }
                }

                //all guests have confirmed their reservation - no table for us
                return false;
            }

            return true;
        }
    }
}
