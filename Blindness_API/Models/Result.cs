namespace Blindness_API.Models
{
    public class Result
    {
        public int ID { get; set; }
       
       
        public string TestResult { get; set; }
        public string Remarks { get; set; }
        public DateTime DateRecorded { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TestID { get; set; }
        public MedicalTest MedicalTest { get; set; }
    }
}
