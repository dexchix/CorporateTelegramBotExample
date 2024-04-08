using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiseBot.Models.Caches
{
    internal class IncidentCache: TelegramCache
    {
        public string Operation {  get; set; }  
        public DateTime DateIncident { get; set; }
        public string Description { get; set; }
    }
}
