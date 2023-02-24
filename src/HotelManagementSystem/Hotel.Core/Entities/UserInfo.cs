﻿using Hotel.Core.Entities.Identity;

namespace Hotel.Core.Entities
{
	public class UserInfo : IEntity
	{
		public int Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		public string? Address { get; set; }
		public string? PostCode { get; set; }
		public string? Notes { get; set; }
		public string? UserId { get; set; }
	
		//Navigaton Property
		public AppUser? AppUser { get; set; }
	}
}