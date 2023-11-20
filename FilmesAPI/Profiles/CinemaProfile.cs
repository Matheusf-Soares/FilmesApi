using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<UpdateCinemaDto, Cinema>();
            CreateMap<Cinema, UpdateCinemaDto>();
            CreateMap<Cinema, ReadCinemaDto > ();
        }
    }
}
