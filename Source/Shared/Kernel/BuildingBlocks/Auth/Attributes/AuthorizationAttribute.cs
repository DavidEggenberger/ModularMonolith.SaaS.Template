namespace Shared.Kernel.BuildingBlocks.Auth.Attributes
{
    public class AuthorizationAttribute : Attribute
    {
        public TenantRole Role { get; set; }
    }
}
