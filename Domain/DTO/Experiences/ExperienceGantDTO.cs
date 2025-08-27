namespace Domain.DTO.Experiences
{
	public class ExperienceGantDTO
	{
		public int ExperienceId { get; set; }
		public IEnumerable<int> WorkerIds { get; set; }
		public DateTimeOffset? TimeStart { get; set; }
		public DateTimeOffset? TimeEnd { get; set; }

		public override int GetHashCode()
		{
			return ExperienceId.GetHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (obj is ExperienceGantDTO experience)
			{
				return this.ExperienceId == experience.ExperienceId;
			}
			return false;
		}
	}
}
