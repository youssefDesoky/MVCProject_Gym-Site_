using System;
using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add Models Mapping Configurations Here
        MapSession();
    }

    private void MapSession()
    {
        CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

        CreateMap<CreateSessionViewModel, Session>();

        CreateMap<UpdateSessionViewModel, Session>().ReverseMap(); // Enable two-way mapping
    }
}
