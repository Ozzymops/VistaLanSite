using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VistaLanSite.Models
{
    public class OverviewModel
    {
        public int ModelExists { get; set; }
        public int UpdatedParticipantId { get; set; }
        public bool OnlyUnpaidParticipants { get; set; }
        public List<Classes.Participant> ParticipantList = new List<Classes.Participant>();
    }
}
