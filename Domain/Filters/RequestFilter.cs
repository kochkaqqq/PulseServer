using Domain.enums;

namespace Domain.Filters
{
	public class RequestFilter
	{
		public int? ClientId { get; set; }
		public string ReasonRequest { get; set; } = string.Empty;
		public string NecessaryFunds { get; set; } = string.Empty;
		public int? ManagerId { get; set; }
		public string InternalInfo { get; set; } = string.Empty;
		public Status Status { get; set; } = Status.None;
		public DoneWorkActType WorkResultType { get; set; } = DoneWorkActType.None;
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public bool? IsDocAttached { get; set; }
	}
}
