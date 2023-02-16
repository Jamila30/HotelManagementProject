namespace Hotel.Core.Entities.Identity
{
    public class AppUser:IdentityUser,ITableEntity
    {
        public string? Fullname { get; set; }
        public bool? IsDeleted { get; set; }
	}
}
