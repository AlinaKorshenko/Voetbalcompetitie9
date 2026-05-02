using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChampionsLeagueTickets.ViewModels
{
    public class AbonnementenIndexVM
    {
        public IEnumerable<AbonnementenInformatieVM> Abonnementen { get; set; } = new List<AbonnementenInformatieVM>();
        public SelectList SeizoenenLijst { get; set; }
        public string? GeselecteerdSeizoenId { get; set; }
    }
}
