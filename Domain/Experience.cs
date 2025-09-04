using Domain.enums;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Experience : IValidatableObject, IArchivable
	{
		public int ExperienceId { get; set; }
		public Request Request { get; set; } = null!;
		public int? MainWorkerId { get; set; }
		public Worker? MainWorker { get; set; }
		public ICollection<Worker> Workers { get; set; } = null!;
		public DateTime Date { get; set; }
		public bool Garant { get; set; } = false;
		public string WorkPlan { get; set; } = string.Empty;
		public string DoneWork { get; set; } = string.Empty;
		public string UsedMaterials { get; set; } = string.Empty;
		public bool IsWorkDone { get; set; } = false;
		public string RemainWork { get; set; } = string.Empty;
		public bool IsWorkPlaceClean { get; set; }
		public bool IsTaskAccepted { get; set; }
		public DateTime? TimeStart { get; set; } = null;
		public DateTime? TimeEnd { get; set; } = null;
		public ReportState ReportState { get; set; } = ReportState.None;

		[JsonIgnore]
		public ICollection<Report>? Reports { get; set; }

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;
			return ExperienceId == ((obj as Experience) ?? new Experience()).ExperienceId;
		}

		public override int GetHashCode()
		{
			return ExperienceId.GetHashCode();
		}

		public override string ToString()
		{
			return "Выезд №" + ExperienceId;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Request == null)
				yield return new ValidationResult($"{ExperienceId} does not have Request");

			if (Workers == null || Workers.Count == 0)
				yield return new ValidationResult($"{ExperienceId} does not have any Workers");
		}
	}
}
