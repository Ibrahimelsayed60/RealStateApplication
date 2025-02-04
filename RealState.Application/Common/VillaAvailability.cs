using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Common
{
    public static class VillaAvailability
    {

        public static int VillaRoomsAvailableCount(int villaId, List<VillaNumber> villaNumbers, DateOnly checkInDate, int nights, List<Booking> bookings)
        {
            List<int> bookingInDates = new List<int>();

            var roomsInVilla = villaNumbers.Where(x => x.VillaId == villaId).Count();

            int finalAvailableRoomForAllNights = int.MaxValue;

            for(int i= 0; i < nights; i++)
            {
                var villasBooked = bookings.Where(u => u.VillaId == villaId
                                                    && u.CheckInDate <= checkInDate.AddDays(i) //  Ensures that the booking's check-in date is on or before a given date
                                                    && u.CheckOutDate > checkInDate.AddDays(i)); // Ensures that the check-out date is after the given date


                foreach(var booking  in villasBooked)
                {
                    if(!bookingInDates.Contains(booking.Id))
                    {
                        bookingInDates.Add(booking.Id);
                    }
                }

                var totalAvailableRooms = roomsInVilla - bookingInDates.Count();

                if(totalAvailableRooms == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomForAllNights > totalAvailableRooms)
                    {
                        finalAvailableRoomForAllNights = totalAvailableRooms;
                    }
                }

            }
            return finalAvailableRoomForAllNights; 

        }

    }
}
