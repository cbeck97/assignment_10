using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment_10.Models
{
    public partial class Team
    {
        public Team()
        {
            Bowlers = new HashSet<Bowler>();
            TourneyMatchEvenLaneTeams = new HashSet<TourneyMatch>();
            TourneyMatchOddLaneTeams = new HashSet<TourneyMatch>();
        }

        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public long? CaptainId { get; set; }

        public virtual ICollection<Bowler> Bowlers { get; set; }
        public virtual ICollection<TourneyMatch> TourneyMatchEvenLaneTeams { get; set; }
        public virtual ICollection<TourneyMatch> TourneyMatchOddLaneTeams { get; set; }
    }
}
