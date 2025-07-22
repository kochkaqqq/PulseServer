using Domain.enums;

namespace Domain
{
	public class Log
	{
		public int LogId { get; set; }
		public LogType LogType { get; set; }
		public string OldVersion { get; set; } = string.Empty;
		public string NewVersion { get; set; } = string.Empty;
		public LogStatus Status { get; set; }
		public ProffesionType AuthorProffesionType { get; set; }
		public int AuthorId { get; set; }
	}
}
