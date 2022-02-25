using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Bug_Tracker_V1._0.Data
{
    public class ApplicationDbContext : IdentityDbContext<AuthenticationUser, AuthenticationRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=localhost;Database=Tacker_core;Username=postgres;Password=Boater3569!");

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }
        public DbSet<AuthenticationRole> AuthenticationRoles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TicketHistory> TicketHistory { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
           
            builder.Entity<UserProject>().HasKey(tu => new { tu.ProjectId, tu.AuthenticationUserId });

        }

        internal System.Threading.Tasks.Task<AuthenticationUser> GetUser(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }

}
