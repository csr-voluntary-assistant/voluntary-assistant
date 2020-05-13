using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Voluntariat.Models.Identity;

namespace Voluntariat.Services.Identity
{
    public class RegistrationGetModel
    {
        public string ReturnUrl { get; set; }

        public RegisterAs RegisterAs { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public List<SelectListItem> AvailableNGOs { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }

        public List<SelectListItem> AvailableServices { get; set; }
    }
}
