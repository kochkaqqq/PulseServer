using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
	public class WorkerDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (obj is WorkerDTO worker)
				return this.Id == worker.Id;
			return false;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
