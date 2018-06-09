using CharacterManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CharacterManager.Startup))]
namespace CharacterManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    UserName = "admin@email.com",
                    Email = "admin@email.com"
                };

                string userPWD = "!2Simple";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "User"
                };
                roleManager.Create(role);

                string userPWD = "!2Simple";

                var user0 = new ApplicationUser
                {
                    UserName = "gamemaster@email.com",
                    Email = "gamemaster@email.com"
                };
                var chkUser0 = UserManager.Create(user0, userPWD);
                if (chkUser0.Succeeded)
                {
                    var result0 = UserManager.AddToRole(user0.Id, "Admin");
                }

                var user1 = new ApplicationUser
                {
                    UserName = "player1@email.com",
                    Email = "player1@email.com"
                };
                var chkUser1 = UserManager.Create(user1, userPWD);
                if (chkUser1.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, "Admin");
                }

                var user2 = new ApplicationUser
                {
                    UserName = "player2@email.com",
                    Email = "player2@email.com"
                };
                var chkUser2 = UserManager.Create(user2, userPWD);
                if (chkUser2.Succeeded)
                {
                    var result2 = UserManager.AddToRole(user2.Id, "Admin");
                }

                var user3 = new ApplicationUser
                {
                    UserName = "player3@email.com",
                    Email = "player3@email.com"
                };
                var chkUser3 = UserManager.Create(user3, userPWD);
                if (chkUser3.Succeeded)
                {
                    var result3 = UserManager.AddToRole(user3.Id, "Admin");
                }

                var user4 = new ApplicationUser
                {
                    UserName = "player4@email.com",
                    Email = "player4@email.com"
                };
                var chkUser4 = UserManager.Create(user4, userPWD);
                if (chkUser4.Succeeded)
                {
                    var result4 = UserManager.AddToRole(user4.Id, "Admin");
                }
            }
        }
    }
}
