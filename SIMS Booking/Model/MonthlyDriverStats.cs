namespace SIMS_Booking.Model
{
    public class MonthlyDriverStats
    {
        public string Month { get; set; }
        public int TotalRides { get; set; }
        public double AveragePrice { get; set; }
        public double AverageTime { get; set; }

        public MonthlyDriverStats() { }

        public MonthlyDriverStats(string month, int totalRides, double averagePrice, double averageTime)
        {
            Month = month;
            TotalRides = totalRides;
            AveragePrice = averagePrice;
            AverageTime = averageTime;
        }
    }
}
