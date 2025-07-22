using System.Security.Cryptography;
using System.Text;

namespace Application.Converters
{
	public static class GetSHAHash
	{
		public static string ComputeSha256Hash(byte[] data)
		{
			using SHA256 sha256 = SHA256.Create();

			byte[] hashBytes = sha256.ComputeHash(data);

			StringBuilder sb = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}
