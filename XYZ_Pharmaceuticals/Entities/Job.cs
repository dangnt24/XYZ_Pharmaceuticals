namespace XYZ_Pharmaceuticals.Entities
{
    public class Job
    {
        public int ID { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string JobDescription { get; set; }
        public string Requirements { get; set; }
        public string SalaryRange { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime? PostedDate { get; set; }
    }
}
