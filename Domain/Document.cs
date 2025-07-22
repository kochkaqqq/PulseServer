using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Document
	{
		public int DocumentId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Extension {  get; set; } = string.Empty;
		public string Hash { get; set; } = string.Empty;
		public byte[] File { get; set; } = null!;

		[JsonIgnore]
		public ICollection<Request> Requests { get; set; }
	}
}
