using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using Voetbalcompetitie9.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ChampionsLeagueTickets.ViewModels;

namespace ChampionsLeagueTickets.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Match, MatchVM>()
            .ForMember(dest => dest.homeTeamName,
                opts => opts.MapFrom(src => src.ThuisTeam.Naam))
            .ForMember(dest => dest.awayTeamName,
                opts => opts.MapFrom(src => src.BezoekendTeam.Naam))
            .ForMember(dest => dest.stadionName,
                opts => opts.MapFrom(src => src.ThuisTeam.Stadion.Naam));

            CreateMap<Stadion, StadionVM>();
            CreateMap<VakType, VakTypeInformatieVM>();
        }

    }
}
