using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VistaLanSite.Classes
{
    public class Participant
    {
        /// <summary>
        /// Participant ID from DB
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Participant's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Participant's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Participant's student number
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// Participant's student class
        /// </summary>
        public string StudentClass { get; set; }

        /// <summary>
        /// Participant's personal phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Does the participant bring a console?
        /// </summary>
        public bool BringsConsole { get; set; }

        /// <summary>
        /// Console hardware details
        /// </summary>
        public string ConsoleDetails { get; set; }

        /// <summary>
        /// Does the participant bring a computer?
        /// </summary>
        public bool BringsComputer { get; set; }

        /// <summary>
        /// Computer hardware details
        /// </summary>
        public string ComputerDetails { get; set; }

        /// <summary>
        /// Has the participant paid yet?
        /// </summary>
        public bool HasPaid { get; set; }
    }
}
