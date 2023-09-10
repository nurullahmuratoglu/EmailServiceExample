using AutoMapper;
using EmailServiceExample.API.DTOs;
using EmailServiceExample.API.Model;

namespace EmailServiceExample.API.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<CreateEmailDto, Email>().ReverseMap();
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<EmailHistory, SendEmailDto>().ReverseMap();
        }
    }
}
