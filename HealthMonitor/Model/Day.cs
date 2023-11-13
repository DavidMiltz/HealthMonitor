namespace Days
{
    public class Day
    {
        public Day()
        {
            City = "Munich";
            Comment = "";
        }
        public DateTime Date { get; set; }

        public string? Drug { get; set; }
        public string City { get; set; }
        public int QualityOfSleep { get; set; }
        public int HealthStatus { get; set; }
        public int MentalStatus { get; set; }
        public int Sport { get; set; }
        public int Merkmal { get; set; }
        public int Alcohol { get; set; }
        public int AirPressure { get; set; }
        public int Temp { get; set; }
        public string Comment { get; set; }
        public string? Food { get; set; }
    }
}