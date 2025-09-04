namespace Domain.DTO
{
	public class RequestListElementDTO
	{
		public int RequestId { get; set; }
		public DateTime Date { get; set; }
		public string ClientName { get; set; } = string.Empty;
		public string ReasonRequest { get; set; } = string.Empty;
		public enums.Status Status { get; set; }
	}
}
