namespace Hotel.Business.DTOs.AccountsDTOs
{
	public class AppUserDto:IDto
	{

		public string? Id { get; set;}
		public string? UserName { get; set;}
		public string? FullName { get; set;}
		public string? Email { get; set;}
		public string? PhoneNumber { get; set;}
		public bool EmailConfirmed { get; set; }
		public DateTime LockOutDate { get; set; }


	}
}
