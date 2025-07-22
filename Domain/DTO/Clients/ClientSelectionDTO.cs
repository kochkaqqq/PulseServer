namespace Domain.DTO.Clients
{
	public class ClientSelectionDTO
	{
		public int ClientId { get; set; }
		public string Name { get; set; } = string.Empty;

		public override string ToString()
		{
			return Name;
		}
	}
}
