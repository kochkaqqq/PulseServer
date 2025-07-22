namespace Domain.Filters
{
	public class ClientFilter
	{
		public string Name { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Contact { get; set; } = string.Empty;
		public string EMail { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
