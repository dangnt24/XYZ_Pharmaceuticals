namespace XYZ_Pharmaceuticals.Entities
{
    public class Candidate
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Resume { get; set; } = string.Empty;
        public string? PersonalDetails { get; set; } = "No yet";
        public string? EducationDetails { get; set; } = "No yet";
    }
}
