using AutoMapper;
using RealState.Domain.Entities;
using RealState.Presentation.ViewModels;

namespace RealState.Presentation.Helpers
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Booking, BookingViewModel>();
        }

    }
}
