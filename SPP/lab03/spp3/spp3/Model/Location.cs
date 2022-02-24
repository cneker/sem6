﻿namespace spp3.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int RaceId { get; set; }
        public Race Race { get; set; }
    }
}
