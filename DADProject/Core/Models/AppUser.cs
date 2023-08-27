﻿using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public List<AppUser> Friends { get; set; } = new List<AppUser>();
    public List<Notification> Notifications { get; set; } = new List<Notification>();
}
