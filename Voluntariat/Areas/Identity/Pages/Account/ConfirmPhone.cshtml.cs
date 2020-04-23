using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariat.Models;
using Voluntariat.Services;

namespace Voluntariat.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ConfirmPhoneModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TwilioVerifyClient twilioVerifyClient;

        public ConfirmPhoneModel(UserManager<ApplicationUser> userManager, TwilioVerifyClient twilioVerifyClient)
        {
            _userManager = userManager;
            this.twilioVerifyClient = twilioVerifyClient;
        }

        public int DialingCode { get; set; }
        public string PhoneNumber { get; set; }

        [BindProperty, Required, Display(Name = "Code")]
        public string VerificationCode { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPhoneNumber();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadPhoneNumber();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var response = await twilioVerifyClient.CheckVerificationCode(DialingCode, PhoneNumber, VerificationCode);
                
                if (response.Success)
                {
                    var identityUser = await _userManager.GetUserAsync(User);
                    identityUser.PhoneNumberConfirmed = true;
                    var updateResult = await _userManager.UpdateAsync(identityUser);

                    if (updateResult.Succeeded)
                    {
                        return RedirectToPage("ConfirmPhoneSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError("", "There was an error confirming the verification code, please try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"There was an error confirming the verification code: {response.Message}");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                    "There was an error confirming the code, please check the verification code is correct and try again");
            }

            return Page();
        }

        private async Task LoadPhoneNumber()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new Exception($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            PhoneNumber = user.PhoneNumber;
            DialingCode = user.DialingCode;
        }
    }
}
