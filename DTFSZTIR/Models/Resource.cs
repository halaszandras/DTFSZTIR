namespace DTFSZTIR.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public int[] TransportTime { get; set; }
        public int[,] SetupTime { get; set; }
        public int IntervalNumber { get; set; }
        public TimeWindow[] Intervals { get; set; }        
    }
}
