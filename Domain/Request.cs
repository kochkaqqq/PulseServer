using Domain.enums;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Request : IValidatableObject, IArchivable
	{
		public int RequestId { get; set; }
		public Client Client { get; set; } = null!;
		public DateTimeOffset Date { get; set; }
		public string ReasonRequest { get; set; } = string.Empty;
		public string NecessaryFunds { get; set; } = string.Empty;
		public Manager Manager { get; set; } = null!;
		public string InternalInfo { get; set; } = string.Empty;
		public Status Status { get; set; } = Status.None;
		public string ActFilePath { get; set; } = string.Empty;
		public DoneWorkActType WorkResultType { get; set; } = DoneWorkActType.None;
		public int? DocumentId { get; set; }
		public Document? Document { get; set; }
		public ICollection<Document>? Files { get; set; }

		[JsonIgnore]
		public ICollection<Experience>? Experiencies { get; set; }

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override bool Equals(object? obj)
		{
			if (obj is Request req)
			{
				if (this.RequestId == req.RequestId)
					return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.RequestId.GetHashCode();
		}

		public override string ToString()
		{
			return "Заявка №" + RequestId;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Manager == null)
				yield return new ValidationResult($"{RequestId} does not have manager");

			if (Status == Status.None)
				yield return new ValidationResult($"Status not choosed for {RequestId}");

			if (WorkResultType == DoneWorkActType.None)
				yield return new ValidationResult($"WorkResultType not choosed for {RequestId}");
		}
	}
}
