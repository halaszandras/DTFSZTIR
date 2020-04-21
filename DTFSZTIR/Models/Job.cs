namespace DTFSZTIR.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int[] ProcessTime { get; set; }
        public int[] StartTime { get; set; }
        public int[] EndTime { get; set; }
        public int DueDate { get; set; }

        public Job()
        {

        }
    }
}
