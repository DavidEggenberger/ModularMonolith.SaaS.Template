using Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain.Exceptions;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Features.Domain;
using Shared.Features.Domain.Exceptions;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain
{
    public class Tenant : AggregateRoot
    {
        public Tenant()
        {

        }

        public override Guid TenantId { get => base.TenantId; }
        public string Name { get; set; }
        public TenantStyling Styling { get; set; }
        public TenantSettings Settings { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public IReadOnlyCollection<TenantMembership> Memberships => memberships.AsReadOnly();
        private List<TenantMembership> memberships = new List<TenantMembership>();
        public IReadOnlyCollection<TenantInvitation> Invitations => invitations.AsReadOnly();
        private List<TenantInvitation> invitations = new List<TenantInvitation>();

        public static async Task<Tenant> CreateTenantWithAdminAsync(string name, Guid adminUserId)
        {
            return new Tenant
            {


            };
        }

        public void AddUser(Guid userId, TenantRole role)
        {

            TenantMembership tenantMembership;
            if ((tenantMembership = memberships.SingleOrDefault(m => m.UserId == userId)) is not null)
            {
                tenantMembership.Role = role;
            }
            else
            {
                memberships.Add(new TenantMembership(userId, role));
            }
        }

        public void ChangeRoleOfUser(Guid userId, TenantRole role)
        {

        }

        public void ChangeRoleOfMember(Guid userId, TenantRole newRole)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }
        }

        public void RemoveUser(Guid userId)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

            memberships.Remove(memberships.Single(m => m.UserId == userId));
        }

        public void InviteUserToRole(Guid userId, TenantRole role)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId))
            {
                throw new UserIsAlreadyMemberException();
            }

            //invitations.Add(new TenantInvitation { UserId = userId, Role = role });
        }

        public async void DeleteTenantMembership(Guid membershipId)
        {
            var tenantMembership = Memberships.SingleOrDefault(t => t.Id == membershipId);
            if (tenantMembership == null)
            {
                throw new NotFoundException();
            }
        }

        public bool CheckIfMember(Guid userId)
        {
            return memberships.Any(membership => membership.UserId == userId);
        }

        public void ThrowIfUserCantDeleteTenant()
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);
        }

        public TenantDTO ToDTO() => new TenantDTO();
        public TenantDetailDTO ToDetailDTO() => new TenantDetailDTO();

    }
}
