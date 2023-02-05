using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public static string CopyFileTo(this IFormFile file,params string[] folders)
		{
			string filename= Guid.NewGuid() + file.FileName;
			string resultPath = string.Empty;
			foreach (var folder in folders)
			{
				resultPath= Path.Combine(resultPath, folder);
			}
			resultPath=Path.Combine(resultPath, filename);

			using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
			{
				file.CopyToAsync(fileStream);
			}
			return filename;
			
		}
	}
}
