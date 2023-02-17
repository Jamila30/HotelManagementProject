namespace Hotel.Core.Entities.Identity
{
    public class AppUser:IdentityUser,ITableEntity
    {
        public AppUser()
        {
            Comments= new HashSet<Comment>();
        }
        public string? Fullname { get; set; }
        public bool? IsDeleted { get; set; }

        //Navigation Property
        public ICollection<Comment>? Comments { get; set; }
	}
}
