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

		public static HashSet<string> Roles= new HashSet<string>();
	}
}
