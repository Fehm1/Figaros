using AutoMapper;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Entities.DTOs.FAQDtos;
using Figaros.Entities.DTOs.ImageDtos;
using Figaros.Entities.DTOs.OurSalonDtos;
using Figaros.Entities.DTOs.PriceDtos;
using Figaros.Entities.DTOs.ProductDtos;
using Figaros.Entities.DTOs.ProfessionDtos;
using Figaros.Entities.DTOs.RequestDtos;
using Figaros.Entities.DTOs.ServiceDtos;
using Figaros.Entities.DTOs.SettingDtos;
using Figaros.Entities.DTOs.SliderDtos;
using Figaros.Entities.DTOs.SponsorDtos;
using Figaros.Entities.DTOs.TimeDtos;

namespace Figaros.Services.AutoMapper
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<About, AboutDto>();
            CreateMap<AboutUpdateDto, About>();
            CreateMap<About, AboutUpdateDto>();

            CreateMap<Booking, BookingDto>();
            CreateMap<BookingPostDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();
            CreateMap<Booking, BookingUpdateDto>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeePostDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();

            CreateMap<FAQ, FAQDto>();
            CreateMap<FAQPostDto, FAQ>();
            CreateMap<FAQUpdateDto, FAQ>();
            CreateMap<FAQ, FAQUpdateDto>();

            CreateMap<Image, ImageDto>();
            CreateMap<ImagePostDto, Image>();
            CreateMap<ImageUpdateDto, Image>();
            CreateMap<Image, ImageUpdateDto>();

            CreateMap<OurSalon, OurSalonDto>();
            CreateMap<OurSalonPostDto, OurSalon>();
            CreateMap<OurSalonUpdateDto, OurSalon>();
            CreateMap<OurSalon, OurSalonUpdateDto>();

            CreateMap<Price, PriceDto>();
            CreateMap<PricePostDto, Price>();
            CreateMap<PriceUpdateDto, Price>();
            CreateMap<Price, PriceUpdateDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, PriceUpdateDto>();

            CreateMap<Profession, ProfessionDto>();
            CreateMap<ProfessionPostDto, Profession>();
            CreateMap<ProfessionUpdateDto, Profession>();
            CreateMap<Profession, ProfessionUpdateDto>();

            CreateMap<Request, RequestDto>();
            CreateMap<RequestPostDto, Request>();

            CreateMap<Service, ServiceDto>();
            CreateMap<ServicePostDto, Service>();
            CreateMap<ServiceUpdateDto, Service>();
            CreateMap<Service, ServiceUpdateDto>();

            CreateMap<Setting, SettingDto>();
            CreateMap<SettingUpdateDto, Setting>();
            CreateMap<Setting, SettingUpdateDto>();

            CreateMap<Slider, SliderDto>();
            CreateMap<SliderPostDto, Slider>();
            CreateMap<SliderUpdateDto, Slider>();
            CreateMap<Slider, SliderUpdateDto>();

            CreateMap<Sponsor, SponsorDto>();
            CreateMap<SponsorPostDto, Sponsor>();
            CreateMap<SponsorUpdateDto, Sponsor>();
            CreateMap<Sponsor, SponsorUpdateDto>();

            CreateMap<Time, TimeDto>();
            CreateMap<TimePostDto, Time>();
            CreateMap<TimeUpdateDto, Time>();
            CreateMap<Time, TimeUpdateDto>();
        }
    }
}
