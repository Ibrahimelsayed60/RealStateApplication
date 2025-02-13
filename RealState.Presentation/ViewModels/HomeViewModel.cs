﻿

using RealState.Domain.Entities;

namespace RealState.Presentation.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Villa>?  VillaList { get; set; }

        public DateOnly CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }

        public int NumberOfNights { get; set; }
    }
}
