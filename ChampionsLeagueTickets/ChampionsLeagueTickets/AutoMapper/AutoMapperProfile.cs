using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.DTO;
using ChampionsLeagueTickets.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChampionsLeagueTickets.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            //matches info page
            CreateMap<Match, MatchVM>()
            .ForMember(dest => dest.homeTeamName,
                opts => opts.MapFrom(src => src.ThuisTeam.Naam))
            .ForMember(dest => dest.awayTeamName,
                opts => opts.MapFrom(src => src.BezoekendTeam.Naam))
            .ForMember(dest => dest.stadionName,
                opts => opts.MapFrom(src => src.ThuisTeam.Stadion.Naam));

            //abonnementen info page
            CreateMap<AbonnementenPrijs, AbonnementenInformatieVM>()
                .ForMember(dest => dest.StadionNaam, opt => opt.MapFrom(src => src.Stadion.Naam))
                .ForMember(dest => dest.SeizoenNaam, opt => opt.MapFrom(src => src.Seizoen.Naam))
                .ForMember(dest => dest.StartDatum, opt => opt.MapFrom(src => src.Seizoen.StartDatum))
                .ForMember(dest => dest.EindDatum, opt => opt.MapFrom(src => src.Seizoen.EindDatum))
                .ForMember(dest => dest.Prijs, opt => opt.MapFrom(src => src.Prijs));

            //api
            //stadion
            CreateMap<Stadion, StadionVM>();
            CreateMap<VakType, VakTypeVM>();

            //users
            CreateMap<AspNetUser, UserInfoResponse>();
            CreateMap<UserInfoResponse, UserVM>();
        }

    }
}
