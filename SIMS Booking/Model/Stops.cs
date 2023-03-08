namespace SIMS_Booking.Model
{
    public class Stops
    {


        public string Start { get; set; }
        public string End { get; set; }

        public Stops() { }

        public Stops(string start, string end)
        {
            Start = start;
            End = end;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Start, End };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Start = values[0];
            End = values[1];
        }
    }
}