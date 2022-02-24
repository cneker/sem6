using System.Collections.Generic;

namespace spp2
{
    class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Racer> Racers { get; set; }
    }
}
