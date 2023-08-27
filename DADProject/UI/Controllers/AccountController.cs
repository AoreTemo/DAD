using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;
using DAL.Data;
using Core.Enums;
using Azure.Identity;

namespace UI.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
       RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var response = new LoginViewModel();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = $"Some properties in {nameof(loginViewModel)} is not valid";

            return View(loginViewModel);
        }

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
                    return RedirectToAction("Index", "Home");
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

    public async Task<IActionResult> Register()
    {
        var response = new RegisterViewModel();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = $"Some properties in {nameof(registerViewModel)} is not valid";

            return View(registerViewModel);
        }

        var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

        if (user != null)
        {
            TempData["Error"] = "This email adress is already in use";

            return View(registerViewModel);
        }

        var newUser = new AppUser()
        {
            Email = registerViewModel.EmailAddress,
            UserName = registerViewModel.Username,
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName
        };

        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (!newUserResponse.Succeeded)
        {
            TempData["Error"] = $"Failed to create {nameof(newUser)}";

            return RedirectToAction("Index", "Home");
        }

        await CreateAppUserRoles();

        //Пока что оставляю админом
        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, Role.Admin.ToString());

        if (!addToRoleResult.Succeeded)
        {
            TempData["Error"] = $"Failed to add to Role: {nameof(Role.Admin)} {nameof(newUser)}";

            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
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