namespace DTFSZTIR.Models
{
    public class PalmerJob
    {
        public int Id { get; set; }
        public int Priority { get; set; }

        public PalmerJob(int id, int priority)
        {
            Id = id;
            Priority = priority;
        }
    }
}
