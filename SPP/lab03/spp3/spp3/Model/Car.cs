using System.Collections.Generic;

namespace spp3.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }

        public List<Racer> Racers { get; set; }
    }
}
