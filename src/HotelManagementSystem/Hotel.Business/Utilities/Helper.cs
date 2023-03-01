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
			if(System.IO.File.Exists(result))
			{
				System.IO.File.Delete(result);
				return true;
			}
			return false;
		}

	//	public static HashSet<string> Roles= new HashSet<string>();
	}
}
