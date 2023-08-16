using AutoMapper;
using BicycleStation.Dtos;

namespace BicycleStation.Model
{
    public class BicycleStationMappingProfile : Profile
    {
        public BicycleStationMappingProfile()
        {
            CreateMap<BicyclePump, BicyclePumpDTO>()/*ForMember(dest => dest.)*/;
            CreateMap<RepairStation, GetRepairStationDTO>();
        }
    }
}
