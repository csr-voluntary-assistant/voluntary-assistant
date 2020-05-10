using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using Voluntariat.Framework.Identity;
using Voluntariat.Models;
using Voluntariat.Services.CloudFileServices;

namespace Voluntariat.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly ISecureCloudFileManager secureCloudFileManager;

        private readonly Data.ApplicationDbContext applicationDbContext;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ISecureCloudFileManager secureCloudFileManager,
            Data.ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.secureCloudFileManager = secureCloudFileManager;

            this.applicationDbContext = applicationDbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public RegisterAs RegisterAs { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public List<SelectListItem> AvailableNGOs { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }

        public List<SelectListItem> AvailableServices { get; set; }

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
            [Required(ErrorMessage = "Please fill in the Address field manually and select an address from the autocomplete list")]
            public double? Longitude { get; set; }

            [HiddenInput]
            public double? Latitude { get; set; }

            public RegisterAs RegisterAs { get; set; }

            [Display(Name = "Action limit (km)")]
            [DisplayFormat(DataFormatString = "{0:[C]}", ApplyFormatInEditMode = true)]
            public decimal RangeInKm { get; set; }

            [Display(Name = "Driver licence")]
            public bool HasDriverLicence { get; set; }

            [Display(Name = "Transportation method")]
            [Required]
            public TransportationMethod TransportationMethod { get; set; }

            [Display(Name = "Other")]
            public string OtherTransportationMethod { get; set; }

            public Guid? NGOID { get; set; }

            public NGORegistrationModel NGORegistrationModel { get; set; }

            [Display(Name = "Activate notifications from other NGOs")]
            public bool ActivateNotificationsFromOtherNGOs { get; set; }
        }

        public async Task OnGetAsync(string registerAs, string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            await FillRegistrationDataAsync(Enum.Parse<RegisterAs>(registerAs));
        }

        public async Task<IActionResult> OnPostAsync(List<IFormFile> files, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    DialingCode = Input.DialingCode,
                    Address = Input.Address,
                    Longitude = Input.Longitude.Value,
                    Latitude = Input.Latitude.Value,
                    HasDriverLicence = Input.HasDriverLicence,
                    TransportationMethod = Input.TransportationMethod,
                    OtherTransportationMethod = Input.OtherTransportationMethod,
                    RangeInKm = Input.RangeInKm
                };

                IdentityResult result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    if (Input.RegisterAs == RegisterAs.NGO)
                    {
                        NGO ngo = new NGO();
                        ngo.ID = Guid.NewGuid();
                        ngo.CreatedByID = Guid.Parse(user.Id);
                        ngo.NGOStatus = NGOStatus.PendingVerification;

                        ngo.IdentificationNumber = Input.NGORegistrationModel.IdentificationNumber;
                        ngo.Name = Input.NGORegistrationModel.Name;
                        ngo.HeadquartersAddress = Input.NGORegistrationModel.HeadquartersAddress;
                        ngo.HeadquartersAddressLatitude = Input.NGORegistrationModel.HeadquartersAddressLatitude;
                        ngo.HeadquartersAddressLongitude = Input.NGORegistrationModel.HeadquartersAddressLongitude;
                        ngo.HeadquartersEmail = Input.NGORegistrationModel.HeadquartersEmail;
                        ngo.HeadquartersPhoneNumber = Input.NGORegistrationModel.HeadquartersPhoneNumber;
                        ngo.DialingCode = Input.NGORegistrationModel.DialingCode;
                        ngo.Website = Input.NGORegistrationModel.Website;

                        ngo.CategoryID = Input.NGORegistrationModel.CategoryID;
                        ngo.ServiceID = Input.NGORegistrationModel.ServiceID;

                        if (files.Any())
                        {
                            ngo.FileIDs = await UploadFiles(files);
                        }

                        applicationDbContext.Add(ngo);
                    }
                    else if (Input.RegisterAs == RegisterAs.Volunteer)
                    {
                        Volunteer volunteer = new Volunteer();
                        volunteer.ID = Guid.Parse(user.Id);
                        volunteer.NGOID = Input.NGOID;
                        volunteer.Name = user.Email;
                        volunteer.VolunteerStatus = VolunteerStatus.PendingVerification;
                        volunteer.ActivateNotificationsFromOtherNGOs = Input.ActivateNotificationsFromOtherNGOs;
                        if (!volunteer.NGOID.HasValue)
                            volunteer.UnaffiliationStartTime = DateTime.UtcNow;

                        applicationDbContext.Add(volunteer);
                    }
                    else if (Input.RegisterAs == RegisterAs.Beneficiary)
                    {
                        // pentru inregistrarea de Beneficiar
                    }

                    await userManager.AddToRoleAsync(user, CustomIdentityRole.Guest);

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

            await FillRegistrationDataAsync(Input.RegisterAs);

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<string> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<string> filePaths = new List<string>();
            List<string> fileIDs = new List<string>();

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

                    string fileName = await secureCloudFileManager.UploadFileAsync(filePath);
                    if (!string.IsNullOrWhiteSpace(fileName) && Guid.TryParse(fileName, out var fileID))
                    {
                        fileIDs.Add(fileID.ToString());
                    }

                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Team file not found:" + e);
                    }
                }
            }

            return string.Join<string>(",", fileIDs);
        }

        private async Task FillRegistrationDataAsync(RegisterAs registerAs)
        {
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            RegisterAs = registerAs;

            if (RegisterAs == RegisterAs.NGO)
            {
                AvailableServices = applicationDbContext.Services.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Name }).ToList();
                AvailableCategories = applicationDbContext.Categories.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Name }).ToList();
            }
            else if (RegisterAs == RegisterAs.Volunteer)
            {
                AvailableNGOs = applicationDbContext.NGOs.Select(o => new SelectListItem { Value = o.ID.ToString(), Text = o.Name }).ToList();
            }
        }
    }

    public class NGORegistrationModel
    {
        [Required]
        [Display(Name = "NGO name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country code")]
        public int DialingCode { get; set; }

        [Required]
        [Display(Name = "Headquarters phone number")]
        public string HeadquartersPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Headquarters email")]
        public string HeadquartersEmail { get; set; }

        [Required]
        [Display(Name = "Headquarters address")]
        public string HeadquartersAddress { get; set; } // sediul social

        [HiddenInput]
        public double HeadquartersAddressLatitude { get; set; }

        [HiddenInput]
        public double HeadquartersAddressLongitude { get; set; }

        [Required]
        [Display(Name = "Identification number")]
        public string IdentificationNumber { get; set; } // CUI

        [Required]
        [Display(Name = nameof(Website))]
        public string Website { get; set; }

        [Required]
        public Guid CategoryID { get; set; }

        [Required]
        public Guid ServiceID { get; set; }
    }

    public enum RegisterAs
    {
        NGO = 0,
        Volunteer = 1,
        Beneficiary = 2
    }
}