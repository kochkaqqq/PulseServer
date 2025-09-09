using Domain.enums;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Report : IValidatableObject, IArchivable
	{
		public int ReportId { get; set; }
		public Worker Worker { get; set; } = null!;
		public Experience Experience { get; set; } = null!;
		public DateTime Date { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string UsedMaterials { get; set; } = string.Empty;
		public string DoneWork { get; set; } = string.Empty;
		public bool IsWorkDone { get; set; }
		public string RemainWork { get; set; } = string.Empty;
		public bool IsWorkplaceClean { get; set; }
		public bool IsWorkAccept { get; set; }
		public ReportStatus Status { get; set; } = ReportStatus.WaitingForAccept;
		public DateTime UpdateOn { get; set; }
		public string[] MediaIds { get; set; }
		public List<MediaFile> MediaFiles { get; set; } = new();

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override string ToString()
		{
			return "Отчет №" + ReportId;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Worker == null)
				yield return new ValidationResult($"{ReportId} does not have Worker");

			if (Experience == null)
				yield return new ValidationResult($"{ReportId} does not have Experience");

			if (DoneWork == string.Empty)
				yield return new ValidationResult($"{ReportId} has empty DoneWork field");

			if (!IsWorkDone && RemainWork == string.Empty)
				yield return new ValidationResult($"At {ReportId} work not done and Report has empty RemainWork");
		}
	}
}
