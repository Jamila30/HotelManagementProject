namespace Hotel.Core.Entities.Identity
{
    public class AppUser:IdentityUser,ITableEntity
    {
        public AppUser()
        {
            Comments= new HashSet<Comment>();
            Reservations = new HashSet<Reservation>();

		}
        public string? Fullname { get; set; }
        public bool? IsDeleted { get; set; }

		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }

		//Navigation Property
		public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public UserInfo? UserInfo { get; set; }  
	}
}
