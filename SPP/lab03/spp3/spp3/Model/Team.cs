using System.Collections.Generic;

namespace spp3.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Racer> Racers { get; set; }
    }
}
