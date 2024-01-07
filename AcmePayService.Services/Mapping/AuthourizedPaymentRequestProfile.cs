using AcmePayService.Domain.Entities;
using AcmePayService.Services.Command.AuthorizePayment;
using AutoMapper;

namespace AcmePayService.Domain.Mapping
{
    public class AuthourizedPaymentRequestProfile : Profile
    {
        public AuthourizedPaymentRequestProfile()
        {
            CreateMap<AuthorizeRequest, Payment>()
           .ForMember(
               dest => dest.Amount,
               opt => opt.MapFrom(src => src.Amount)
           )
           .ForMember(
               dest => dest.Currency,
               opt => opt.MapFrom(src => src.Currency)
           )
           .ForMember(
               dest => dest.CardHolderNumber,
               opt => opt.MapFrom(src => src.CardHolderNumber)
           )
            .ForMember(
               dest => dest.HolderName,
               opt => opt.MapFrom(src => src.HolderName)
           )
              .ForMember(
               dest => dest.ExpirationMonth,
               opt => opt.MapFrom(src => src.ExpirationMonth)
           )
                .ForMember(
               dest => dest.ExpirationYear,
               opt => opt.MapFrom(src => src.ExpirationYear)
           )
           .ForMember(
               dest => dest.CVV,
               opt => opt.MapFrom(src => src.CVV)
           )
           .ForMember(
               dest => dest.OrderReference,
               opt => opt.MapFrom(src => src.OrderReference)
           );           
        }
    }
}
