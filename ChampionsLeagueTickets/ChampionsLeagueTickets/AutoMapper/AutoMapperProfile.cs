using AutoMapper;
using ChampionsLeagueTickets.Domain.EntitiesDB;
using ChampionsLeagueTickets.Services.DTO;
using ChampionsLeagueTickets.ViewModels;
using ChampionsLeagueTickets.ViewModels.Abonnementen;
using ChampionsLeagueTickets.ViewModels.order;
using ChampionsLeagueTickets.ViewModels.Stadion;
using ChampionsLeagueTickets.ViewModels.Zitplaatsen;
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
                .ForMember(dest => dest.Prijs, opt => opt.MapFrom(src => src.Prijs))
                .ForMember(dest => dest.VakNummer, opt => opt.MapFrom(src => src.VakNummer))
                .ForMember(dest => dest.VakNaam, opt => opt.MapFrom(src => src.VakNummerNavigation.Omschrijving));

            //api
            //stadion
            CreateMap<Stadion, StadionVM>();
            CreateMap<VakType, VakTypeVM>();

            //users
            CreateMap<AspNetUser, UserInfoResponse>();
            CreateMap<UserInfoResponse, UserVM>();

            //orders
            CreateMap<Zitplaatsen, ZitplaatsVM>()
                .ForMember(dest => dest.VakOmschrijving,
                    opt => opt.MapFrom(src => src.VakNummerNavigation.Omschrijving));

            CreateMap<Ticket, OrderTicketVM>()
                .ForMember(dest => dest.MatchID, opt => opt.MapFrom(src => src.MatchId))
                .ForMember(dest => dest.Prijs, opt => opt.MapFrom(src => src.Prijs))
                .ForMember(dest => dest.Zitplaats, opt => opt.MapFrom(src => src.Zitplaatsen))
                .ForMember(dest => dest.ThuisTeam, opt => opt.MapFrom(src => src.Match.ThuisTeam.Naam))
                .ForMember(dest => dest.BezoekTeam, opt => opt.MapFrom(src => src.Match.BezoekendTeam.Naam))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Match.DatumTijdStartMatch))
                .ForMember(dest => dest.Stadion, opt => opt.MapFrom(src => src.Match.ThuisTeam.Stadion.Naam));

            CreateMap<Abonnementen, OrderAbonementVM>()
                .ForMember(dest => dest.AbonnementId, opt => opt.MapFrom(src => src.AbonnementId))
                .ForMember(dest => dest.Zitplaats, opt => opt.MapFrom(src => src.Zitplaatsen))
                .ForMember(dest => dest.SeizoenNaam, opt => opt.MapFrom(src => src.Seizoen.Naam))
                .ForMember(dest => dest.StadionNaam, opt => opt.MapFrom(src => src.Stadion.Naam));

            CreateMap<Orderlijnen, OrderLijnVM>()
                .ForMember(dest => dest.orderLijnNummer, opt => opt.MapFrom(src => src.OrderLijnNummer))
                .ForMember(dest => dest.bedrag, opt => opt.MapFrom(src => src.Bedrag))
                .ForMember(dest => dest.Ticket, opt => opt.MapFrom(src => src.Ticket))
                .ForMember(dest => dest.Abonement, opt => opt.MapFrom(src => src.Abonnementen));

            CreateMap<Order, OrderVM>()
                .ForMember(dest => dest.OrderLijnen, opt => opt.MapFrom(src => src.Orderlijnens));
        }
    }
}
