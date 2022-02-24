namespace spp2
{
    class Racer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RaceId { get; set; }
        public Race Race { get; set; }


        public int CarId { get; set; }
        public Car Car { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
