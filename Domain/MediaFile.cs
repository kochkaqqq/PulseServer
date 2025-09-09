using System.Text.Json.Serialization;

namespace Domain
{
	public class MediaFile
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string MimeType {  get; set; } = string.Empty;

		public FileStream File { get; set; }

		[JsonIgnore]
		public Report Report { get; set; }
	}
}
