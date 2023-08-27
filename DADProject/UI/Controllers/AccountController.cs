using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;
using DAL.Data;
using Core.Enums;

namespace UI.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await CreateAppUserRoles();
        var response = new LoginViewModel();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

        if (user != null)
        {
            //User is found and we check password
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (passwordCheck)
            {
                //Password is corrent and we sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Race");
                }
            }
            //Wrong password
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginViewModel);
        }
        //User wasn't found
        TempData["Error"] = "Wrong credentials. Please, try again";
        return View(loginViewModel);
    }
    private async Task CreateAppUserRoles()
    {
        if (!await _roleManager.RoleExistsAsync(Role.Admin.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
        }

        if (!await _roleManager.RoleExistsAsync(Role.User.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
        }

    }
}

