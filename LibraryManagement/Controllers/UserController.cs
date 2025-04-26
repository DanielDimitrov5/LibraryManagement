using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using LibraryManagement.Data.ViewModels;
using LibraryManagement.Exceptions;
using LibraryManagement.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, UserManager<User> userManager, ILogger<UserController> logger)
    {
        _userService = userService;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> All()
    {
        List<UserViewModel> usersViewModels = new List<UserViewModel>();

        try
        {
            ICollection<User> users = await _userService.GetAllUsersAsync();

            foreach (User user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var viewModel = new UserViewModel
                {
                    Username = user.UserName ?? throw new ArgumentNullException(nameof(user.UserName)),
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles.ToList(),
                };

                usersViewModels.Add(viewModel);
            }
        }
        catch (Exception e)
        {
            TempData["Error"] = "An unexpected error occured!";
            _logger.Log(LogLevel.Error, $"UserController::Edit\r\n{e.Message}");
        }
        
        return View(usersViewModels);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> BorrowBook(int id)
    {
        await TryCatchHelper.TryCatchAsync(
            async () => await _userService.BorrowBookAsync(id),
            _logger,
            this,
            nameof(BorrowBook)
        );

        return RedirectToAction("All", "Book");
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ReturnBook(int id)
    {
        await TryCatchHelper.TryCatchAsync(
            async () => await _userService.ReturnBookAsync(id),
            _logger,
            this,
            nameof(ReturnBook)
        );
        
        return RedirectToAction("MyBooks", "Book");
    }
}