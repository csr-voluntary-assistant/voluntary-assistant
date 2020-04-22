using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio.Rest.Verify.V2.Service;
using Voluntariat.Models;

namespace Voluntariat.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ConfirmPhoneSuccessModel : PageModel
    {
    }
}
