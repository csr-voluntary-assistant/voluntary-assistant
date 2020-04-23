using System;
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
    public class VerifyPhoneModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TwilioVerifyClient verifyClient;

        public VerifyPhoneModel(UserManager<ApplicationUser> userManager, TwilioVerifyClient verifyClient)
        {
            _userManager = userManager;
            this.verifyClient = verifyClient;
        }

        public string PhoneNumber { get; set; }
        public int CountryCode { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPhoneNumber();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadPhoneNumber();

            try
            {
                var response = await verifyClient.StartVerification(CountryCode, PhoneNumber);                

                if (response.Success)
                {
                    return RedirectToPage("ConfirmPhone");
                }
                
                ModelState.AddModelError("", "There was an error sending the verification code");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("",
                    "There was an error sending the verification code, please check the phone number is correct and try again");
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
            var phone = user.PhoneNumber.Split(";");
            PhoneNumber = phone[1];
            CountryCode = int.Parse(phone[0]);
            
        }
    }
}
