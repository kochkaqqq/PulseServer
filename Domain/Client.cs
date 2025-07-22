using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
	public class Client : IValidatableObject, IArchivable
	{
		public int ClientId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Contact { get; set; } = string.Empty;
		public string EMail { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
		[JsonIgnore]
		public ICollection<Request>? Requests { get; set; }

		[JsonIgnore]
		public bool IsArchive { get; set; } = false;

		public override bool Equals(object? obj)
		{
			if (obj is Client client)
				return this.ClientId == client.ClientId;
			return false;
		}

		public override int GetHashCode()
		{
			return ClientId.GetHashCode();
		}

		public override string ToString()
		{
			return Name;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			yield break;
		}
	}
}
