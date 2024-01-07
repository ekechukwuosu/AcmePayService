using AcmePayService.Domain.DTO;
using AcmePayService.Domain.Entities;
using AutoMapper;

namespace AcmePayService.Services.Mapping
{
    public class PaymentResponseProfile : Profile
    {
        public PaymentResponseProfile()
        {
            CreateMap<Payment, PaymentDTO>()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => src.Id)
           )
           .ForMember(
               dest => dest.Status,
               opt => opt.MapFrom(src => src.Status)
           )
           ;
        }
    }
}
