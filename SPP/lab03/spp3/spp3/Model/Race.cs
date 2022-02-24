using System.Collections.Generic;

namespace spp3.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Location Location { get; set; }
        public List<Racer> Racers { get; set; }
    }
}
