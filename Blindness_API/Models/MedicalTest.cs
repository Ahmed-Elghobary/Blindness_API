namespace Blindness_API.Models
{
    public class MedicalTest
    {
        public int ID { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public DateTime DateConducted { get; set; }

        public List<Result> Results { get; set; }
    }
}
