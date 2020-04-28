using System;

namespace Voluntariat.Framework.Identity
{
    public class Identity
    {
        public Guid ID { get; set; }

        public string Role { get; set; }

        public Guid NGOID { get; set; }

        public string NGOName { get; set; }
    }

    public static class IdentityConstants
    {
        public const string IdentityKey = "Voluntariat_IdentityClaim_Key";
    }

    public static class IdentityRole
    {
        public const string Admin = "Admin"; // TODO - Security - Rename to PlatformAdmin

        public const string NGOAdmin = "NGOAdmin";

        public const string Volunteer = "Volunteer";

        public const string Beneficiary = "Beneficiary";

        public const string Guest = "Guest";
    }
}
