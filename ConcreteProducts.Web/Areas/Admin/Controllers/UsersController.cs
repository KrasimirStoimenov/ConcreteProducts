namespace ConcreteProducts.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ConcreteProducts.Web.Areas.Admin.Models.Users;
    using System.Data;

    public class UsersController : AdminController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> All()
        {
            var usersViewMode = new List<UserViewModel>();
            var loggedInUser = await this.userManager.GetUserAsync(HttpContext.User);
            var allOtherUsers = this.userManager.Users
                    .Where(u => u.Id != loggedInUser.Id)
                    .ToList();

            foreach (var user in allOtherUsers)
            {
                var role = await this.userManager.GetRolesAsync(user);
                var currentUser = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role.FirstOrDefault()
                };

                usersViewMode.Add(currentUser);
            }

            return View(usersViewMode);
        }

        public async Task<IActionResult> Promote(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var role = await this.userManager.GetRolesAsync(user);

            await this.userManager.RemoveFromRolesAsync(user, role);
            await this.userManager.AddToRoleAsync(user, "Employee");

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Demote(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var role = await this.userManager.GetRolesAsync(user);

            await this.userManager.RemoveFromRolesAsync(user, role);
            await this.userManager.AddToRoleAsync(user, "Basic");

            return RedirectToAction(nameof(All));
        }
    }
}
