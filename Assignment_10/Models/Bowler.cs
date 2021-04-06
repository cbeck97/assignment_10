using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment_10.Models
{
    public partial class Bowler
    {
        public Bowler()
        {
            BowlerScores = new HashSet<BowlerScore>();
        }

        public long BowlerId { get; set; }
        public string BowlerLastName { get; set; }
        public string BowlerFirstName { get; set; }
        public string BowlerMiddleInit { get; set; }
        public string BowlerAddress { get; set; }
        public string BowlerCity { get; set; }
        public string BowlerState { get; set; }
        public string BowlerZip { get; set; }
        public string BowlerPhoneNumber { get; set; }
        public long? TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<BowlerScore> BowlerScores { get; set; }
    }
}
