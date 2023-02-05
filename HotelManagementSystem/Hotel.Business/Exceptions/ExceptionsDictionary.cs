namespace Hotel.Business.Exceptions
{
	public static class ExceptionsDictionary
	{
		public static Exception IncorrectFileFFException { get; private set; }
		//	public static Exception InccorrectFileFormatException { get; private set; }
		public static Dictionary<string, Exception> MyExceptions = new Dictionary<string, Exception>(){
			   {"Enter Suitable File Size",IncorrectFileFFException }
		};
	}



}
