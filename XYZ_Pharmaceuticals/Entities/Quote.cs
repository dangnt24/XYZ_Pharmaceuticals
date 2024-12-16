namespace XYZ_Pharmaceuticals.Entities
{
    public class Quote
    {
		public int ID { get; set; }
		public string FullName { get; set; }
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Comments { get; set; }
		public string? Status { get; set; } // Pending, Approved, Rejected
		public string? AdminFeedback { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}