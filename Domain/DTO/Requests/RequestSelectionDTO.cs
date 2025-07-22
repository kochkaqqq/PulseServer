namespace Domain.DTO.Requests
{
	public class RequestSelectionDTO
	{
		public int RequestId { get; set; }
		public int ClientId { get; set; }
		public string ClientName { get; set; } = string.Empty;
		public string RequestTitle { get; set; } = string.Empty;

		public override string ToString()
		{
			return RequestTitle;
		}
	}
}
