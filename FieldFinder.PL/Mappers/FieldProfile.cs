using AutoMapper;
using FieldFinder.DAL.Entities;
using FieldFinder.PL.Models;

namespace FieldFinder.PL.Mappers
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<FieldViewModel, Field>().ReverseMap();

        }
    }
}
