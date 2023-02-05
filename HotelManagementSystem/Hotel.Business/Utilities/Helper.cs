using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Utilities
{
	public static class Helper
	{
		public static bool DeleteFile(params string[] paths)
		{
			string result=string.Empty;
			foreach (var path in paths)
			{
				result = Path.Combine(result, path);
			}
			if(File.Exists(result))
			{
				File.Delete(result);
				return true;
			}
			return false;
		}
	}
}
