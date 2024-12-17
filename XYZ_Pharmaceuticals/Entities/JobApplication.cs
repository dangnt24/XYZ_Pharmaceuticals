namespace XYZ_Pharmaceuticals.Entities
{
    public class JobApplication
    {
        public int ID { get; set; }
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public string Status { get; set; } = "Pending";
        public string? ResumeFilePath { get; set; }
    }
}
