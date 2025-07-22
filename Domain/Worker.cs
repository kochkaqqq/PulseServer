using Domain.Interfaces;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Worker : IValidatableObject, IArchivable
	{
		public int WorkerId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int ShiftSalary { get; set; }
		public int HourSalary { get; set; }
		[JsonIgnore]
		public ICollection<Experience>? Experiencies { get; set; }
		[JsonIgnore]
		public ICollection<Report>? Reports { get; set; }
		public string ApiKey { get; set; } = string.Empty;

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override string ToString()
		{
			return Name.ToString();
		}

		public override bool Equals(object? obj)
		{
			if (obj is Worker worker)
				return this.WorkerId == worker.WorkerId;
			return false;
		}

		public override int GetHashCode()
		{
			return WorkerId.GetHashCode();
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (ShiftSalary < 0)
				yield return new ValidationResult($"{WorkerId}.{Name} has negative shift salary");
			if (HourSalary < 0)
				yield return new ValidationResult($"{WorkerId}.{Name} has negative hour salary");
		}
	}
}
