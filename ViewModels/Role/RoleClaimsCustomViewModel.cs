using Coza_Ecommerce_Shop.Models.Entities;

namespace Coza_Ecommerce_Shop.ViewModels.Role
{
    public class RoleClaimsCustomViewModel
    {
        // id role
        public string Id { set; get; }
        public string RoleName { set; get; }
        public List<ClaimGroup> ClaimGroups { get; set; } = new List<ClaimGroup>();
    }


    public class ClaimCheckBoxItem
    {
        public string ClaimName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsSelected { get; set; }
        public string TypeClaim { get; set; }
    }

    public class ClaimGroup
    {
        public string TypeClaim { get; set; }
        public List<ClaimCheckBoxItem> Claims { get; set; }
    }

    public class ClaimCustomGroup
    {
        public string TypeClaim { get; set; }
        public List<ClaimsCustom> Claims { get; set; }
    }

}
