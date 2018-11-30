using AutoMapper;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Business, BusinessDto>();
            CreateMap<BusinessDto, Business>();

            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();

            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
        }
    }
}
