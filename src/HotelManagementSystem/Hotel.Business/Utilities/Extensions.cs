
namespace Hotel.Business.Utilities
{
	public static class Extensions
	{
		public static bool CheckFileSize(this IFormFile file,int kByte)
		{
			return file.Length/1024 < kByte;
		}
		public static bool CheckFileFormat(this IFormFile file, string format)
		{
			return file.ContentType.Contains(format);
		}
		public static async Task<string> CopyFileToAsync(this IFormFile file,string wwwroot,params string[] folders)
		{
			try
			{
				string filename = Guid.NewGuid() + file.FileName;
				string resultPath = wwwroot;
				foreach (var folder in folders)
				{
					resultPath = Path.Combine(resultPath, folder);
				}
				resultPath = Path.Combine(resultPath, filename);

				using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
				{
					 await file.CopyToAsync(fileStream);
				}
				return filename;
			}
			catch (Exception)
			{
				throw;
			}
			
		}
	}
}
