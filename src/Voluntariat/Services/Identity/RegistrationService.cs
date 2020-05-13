using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;
using Voluntariat.Models.Identity;
using Voluntariat.Services.CloudFileServices;

namespace Voluntariat.Services.Identity
{
    public class RegistrationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegistrationService> logger;
        private readonly IEmailSender emailSender;
        private readonly ISecureCloudFileManager secureCloudFileManager;

        private readonly Data.ApplicationDbContext applicationDbContext;
        private readonly IUrlHelper urlHelper;

        public RegistrationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegistrationService> logger,
            IEmailSender emailSender,
            ISecureCloudFileManager secureCloudFileManager,
            Data.ApplicationDbContext applicationDbContext, 
            IUrlHelper urlHelper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.secureCloudFileManager = secureCloudFileManager;

            this.applicationDbContext = applicationDbContext;
            this.urlHelper = urlHelper;
        }

        public async Task<RegistrationGetModel> GetRegistrationModelAsync(RegisterAs registerAs, string returnUrl)
        {
            RegistrationGetModel registrationGetModel = new RegistrationGetModel
            {
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),

                RegisterAs = registerAs
            };

            if (registrationGetModel.RegisterAs == RegisterAs.NGO)
            {
                registrationGetModel.AvailableServices = applicationDbContext.Services.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Name }).ToList();
                registrationGetModel.AvailableCategories = applicationDbContext.Categories.Select(x => new SelectListItem() { Value = x.ID.ToString(), Text = x.Name }).ToList();
            }
            else if (registrationGetModel.RegisterAs == RegisterAs.Volunteer)
            {
                registrationGetModel.AvailableNGOs = applicationDbContext.NGOs.Select(o => new SelectListItem { Value = o.ID.ToString(), Text = o.Name }).ToList();
            }
            registrationGetModel.ReturnUrl = returnUrl;
            return registrationGetModel;
        }

        public async Task<IdentityResult> SaveUser(UserRegistrationModel Input)
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

            var createdUser = await userManager.CreateAsync(user, Input.Password);

            await userManager.AddToRoleAsync(user, CustomIdentityRole.Guest);
            if (userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                
                await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by entering the following code in the app:{code}");
            }
            return createdUser;
        }

        public async Task<NGO> SaveNgo(string userId, NGORegistrationModel ngoRegistrationModel, List<IFormFile> files)
        {
            NGO ngo = new NGO();
            ngo.ID = Guid.NewGuid();
            ngo.CreatedByID = Guid.Parse(userId);
            ngo.NGOStatus = NGOStatus.PendingVerification;

            ngo.IdentificationNumber = ngoRegistrationModel.IdentificationNumber;
            ngo.Name = ngoRegistrationModel.Name;
            ngo.HeadquartersAddress = ngoRegistrationModel.HeadquartersAddress;
            ngo.HeadquartersAddressLatitude = ngoRegistrationModel.HeadquartersAddressLatitude;
            ngo.HeadquartersAddressLongitude = ngoRegistrationModel.HeadquartersAddressLongitude;
            ngo.HeadquartersEmail = ngoRegistrationModel.HeadquartersEmail;
            ngo.HeadquartersPhoneNumber = ngoRegistrationModel.HeadquartersPhoneNumber;
            ngo.DialingCode = ngoRegistrationModel.DialingCode;
            ngo.Website = ngoRegistrationModel.Website;

            ngo.CategoryID = ngoRegistrationModel.CategoryID;
            ngo.ServiceID = ngoRegistrationModel.ServiceID;

            if (files.Any())
            {
                ngo.FileIDs = await UploadFiles(files);
            }

            applicationDbContext.Add(ngo);
            await applicationDbContext.SaveChangesAsync();
            return ngo;
        }

        public async Task<Volunteer> SaveVolunteerAsync(string userId, string email, Guid? NgoId,bool activateNotificationsFromOtherNGOs)
        {
            Volunteer volunteer = new Volunteer();
            volunteer.ID = Guid.Parse(userId);
            volunteer.NGOID = NgoId;
            volunteer.Name = email;
            volunteer.VolunteerStatus = VolunteerStatus.PendingVerification;
            volunteer.ActivateNotificationsFromOtherNGOs = activateNotificationsFromOtherNGOs;
            if (!volunteer.NGOID.HasValue)
                volunteer.UnaffiliationStartTime = DateTime.UtcNow;

            applicationDbContext.Add(volunteer);
            await applicationDbContext.SaveChangesAsync();
            return volunteer;
        }

        public async Task<string> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<string> fileIDs = new List<string>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    fileIDs.Add(await UploadFile(formFile));
                }
            }

            return string.Join<string>(",", fileIDs);
        }

        private async Task<string> UploadFile(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            string cloudFileId = await secureCloudFileManager.UploadFileAsync(filePath);

            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
            }

            return cloudFileId;
        }
    }


}
