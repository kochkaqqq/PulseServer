using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Manager : IArchivable
	{
		public int ManagerId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		[JsonIgnore]
		public ICollection<Request>? Requests { get; set; }
		public string ApiKey { get; set; } = string.Empty;

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override string ToString()
		{
			return Name;
		}
	}
}
