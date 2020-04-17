using System;

namespace Volutnariat.Framework.Identity
{
    public class Identity
    {
        public Guid ID { get; set; }

        public string Role { get; set; }
    }

    public static class IdentityConstants
    {
        public const string IdentityKey = "Voluntariat_IdentityClaim_Key";
    }
}
