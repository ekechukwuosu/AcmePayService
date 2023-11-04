using AcmePayService.Domain.Command.Requests;
using AcmePayService.Infrastructure.Data.Models;
using AutoMapper;

namespace AcmePayService.Domain.Mapping
{
    public class CaptureAndVoidRequestProfile : Profile
    {
        public CaptureAndVoidRequestProfile()
        {
            CreateMap<CaptureAndVoidCommandRequest, Payment>()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => src.Id)
           )
           .ForMember(
               dest => dest.OrderReference,
               opt => opt.MapFrom(src => src.OrderReference)
           )
           ;
        }
    }
}
