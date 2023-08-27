using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        
        DbSet<AppUser> AppUsers { get; set; } = null!;
        DbSet<Comment> Comments { get; set; } = null!;
        DbSet<Post> Posts { get; set; } = null!;
        DbSet<Notification> Notifications { get; set; } = null!;
        DbSet<Message> Messages { get; set; } = null!;
        DbSet<Media> Medias { get; set; } = null!;
    }
}
