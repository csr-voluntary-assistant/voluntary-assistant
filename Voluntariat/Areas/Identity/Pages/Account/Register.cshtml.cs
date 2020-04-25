using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Voluntariat.Models;

namespace Voluntariat.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        private readonly Data.ApplicationDbContext applicationDbContext;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            Data.ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;

            this.applicationDbContext = applicationDbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public RegisterAs RegisterAs { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            [Required]
            public string PhoneNumber { get; set; }

            [Display(Name = "Country Code")]
            [Required]
            public int DialingCode { get; set; }

            [Display(Name = "Address")]
            [Required]
            public string Address { get; set; }

            [HiddenInput]
            public double Longitude { get; set; } = 0;

            [HiddenInput]
            public double Latitude { get; set; } = 0;

            public RegisterAs RegisterAs { get; set; }

            [Display(Name = "Action limit (km)")]
            [DisplayFormat(DataFormatString = "{0:[C]}", ApplyFormatInEditMode = true)]
            public decimal ActionLimit { get; set; }

            [Display(Name = "Driver licence")]
            public bool HasDriverLicence { get; set; }

            [Display(Name = "Transportation Method")]
            [Required]
            public TransportationMethod TransportationMethod { get; set; } = TransportationMethod.None;

            [Display(Name = "Other...")]
            public string OtherTransportationMethod { get; set; }
        }

        public async Task OnGetAsync(string registerAs, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            RegisterAs = Enum.Parse<RegisterAs>(registerAs);
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    DialingCode = Input.DialingCode,
                    Address = Input.Address,
                    Longitude = Input.Longitude,
                    Latitude = Input.Latitude,
                    ActionLimit = Input.ActionLimit,
                    HasDriverLicence = Input.HasDriverLicence,
                    TransportationMethod = Input.TransportationMethod,
                    OtherTransportationMethod = Input.OtherTransportationMethod
                };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    if (Input.RegisterAs == RegisterAs.NGO)
                    {
                        Ong ong = new Ong();
                        ong.ID = Guid.NewGuid();
                        ong.CreatedByID = Guid.Parse(user.Id);
                        ong.OngStatus = OngStatus.PendingVerification;
                        ong.Name = $"Ong by {user.Email}";

                        applicationDbContext.Add(ong);
                    }
                    else if (Input.RegisterAs == RegisterAs.Volunteer)
                    {
                        // vine Dia si adauga partea pentru Voluntar
                    }
                    else if (Input.RegisterAs == RegisterAs.Beneficiary)
                    {
                        // pentru inregistrarea de Beneficiar
                    }

                    await applicationDbContext.SaveChangesAsync();

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, DisplayConfirmAccountLink = false });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
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

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<string> filePaths = new List<string>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            StatusMessage = "Files successfully uploaded";

            return RedirectToPage();
        }
    }

    public enum RegisterAs
    {
        NGO = 0,
        Volunteer = 1,
        Beneficiary = 2
    }
}