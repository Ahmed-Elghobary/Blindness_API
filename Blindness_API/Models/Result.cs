namespace Blindness_API.Models
{
    public class Result
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int TestID { get; set; }
        public string TestResult { get; set; }
        public string Remarks { get; set; }
        public DateTime DateRecorded { get; set; }

        public LocalUser Patient { get; set; }
        public MedicalTest MedicalTest { get; set; }
    }
}
