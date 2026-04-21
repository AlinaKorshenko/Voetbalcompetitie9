using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTickets.Services.DTO
{
    public class ZitplaatsDto
    {
        public string ZitplaatsId { get; set; }
        public string VakNummer { get; set; }
        public string RijNummer { get; set; }
        public string StoelNummer { get; set; }
        public bool IsBezet { get; set; }
    }
}
