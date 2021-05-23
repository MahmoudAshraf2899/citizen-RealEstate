using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Real_Estate_System.Models;

namespace Real_Estate_System.Areas.Identity.Pages.Account
{
    //[Authorize(Roles = "Admin,Sales,CallCenter,Engineer,Workers")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            
            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Phone Number2")]
            public string PhoneNumber2 { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsSales { get; set; }
            public bool salesMember { get; set; }
            public bool IsEngineer { get; set; }
            public bool engineerMember { get; set; }
            public bool IsCallCenter { get; set; }
            public bool callcenterMember { get; set; }
            public bool IsWorkers { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber=Input.PhoneNumber, PhoneNumber2=Input.PhoneNumber2 };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //Add Roles 
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    if (!await _roleManager.RoleExistsAsync("Sales"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Sales"));
                    }
                    if (!await _roleManager.RoleExistsAsync("salesMember"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("salesMember"));
                    }
                    if (!await _roleManager.RoleExistsAsync("Engineer"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Engineer"));
                    }
                    if (!await _roleManager.RoleExistsAsync("engineerMember"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("engineerMember"));
                    }
                    if (!await _roleManager.RoleExistsAsync("CallCenter"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("CallCenter"));
                    }
                    if (!await _roleManager.RoleExistsAsync("callcenterMember"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("callcenterMember"));
                    }
                    if (!await _roleManager.RoleExistsAsync("Workers"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Workers"));
                    }
                    if (!await _roleManager.RoleExistsAsync("Excutive"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Excutive"));
                    }

                    //Assign User to Role as per the check box selection

                    if (Input.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }

                    if (Input.IsSales)
                    {
                        await _userManager.AddToRoleAsync(user, "Sales");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }
                    if (Input.salesMember)
                    {
                        await _userManager.AddToRoleAsync(user, "salesMember");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }

                    if (Input.IsEngineer)
                    {
                        await _userManager.AddToRoleAsync(user, "Engineer");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }
                    if (Input.engineerMember)
                    {
                        await _userManager.AddToRoleAsync(user, "engineerMember");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }

                    if (Input.IsCallCenter)
                    {
                        await _userManager.AddToRoleAsync(user, "CallCenter");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }
                    if (Input.callcenterMember)
                    {
                        await _userManager.AddToRoleAsync(user, "callcenterMember");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }

                    if (Input.IsWorkers)
                    {
                        await _userManager.AddToRoleAsync(user, "Workers");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Excutive");

                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Users", new { area = "" });

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
