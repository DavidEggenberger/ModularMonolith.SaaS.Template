using Modules.TenantIdentity.DomainFeatures.Domain.Exceptions;
using Shared.DomainFeatures;
using Shared.DomainFeatures.Attributes;
using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.BuildingBlocks.Authorization.Services;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
{
    [AggregateRoot]
    public class Tenant : Entity
    {
        private Tenant() { }
        public Tenant(string name)
        {
            Name = name;
        }
        public Tenant(IServiceProvider serviceProvider)
        {

        }
        public IUserAuthorizationService TenantAuthorizationService { get; set; }

        public override Guid TenantId { get => base.TenantId; }
        public string Name { get; set; }
        public TenantStyling Styling { get; set; }
        public TenantSettings Settings { get; set; }
        public SubscriptionPlanType CurrentSubscriptionPlanType => tenantSubscriptions.Single(t => t.Status != SubscriptionStatus.Inactive).SubscriptionPlanType;
        public IReadOnlyCollection<TenantMembership> Memberships => memberships.AsReadOnly();
        private List<TenantMembership> memberships = new List<TenantMembership>();
        public IReadOnlyCollection<TenantInvitation> Invitations => invitations.AsReadOnly();
        private List<TenantInvitation> invitations = new List<TenantInvitation>();
        public IReadOnlyCollection<TenantSubscription> TenantSubscriptions => tenantSubscriptions.AsReadOnly();
        private List<TenantSubscription> tenantSubscriptions = new List<TenantSubscription>();

        public void AddUser(Guid userId, TenantRole role)
        {
            TenantAuthorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

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
        public void ChangeRoleOfMember(Guid userId, TenantRole newRole)
        {
            TenantAuthorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

        }
        public void RemoveUser(Guid userId)
        {
            TenantAuthorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

            memberships.Remove(memberships.Single(m => m.UserId == userId));
        }
        public void InviteUserToRole(Guid userId, TenantRole role)
        {
            TenantAuthorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId))
            {
                throw new UserIsAlreadyMemberException();
            }

            invitations.Add(new TenantInvitation { UserId = userId, Role = role });
        }
        public void AddSubscription(string stripeSubscriptionId, SubscriptionPlanType type, DateTime startDate, DateTime endDate, bool isTrial)
        {
            foreach (var subscription in tenantSubscriptions)
            {
                subscription.Status = SubscriptionStatus.Inactive;
            }
            tenantSubscriptions.Add(new TenantSubscription
            {
                StripeSubscriptionId = stripeSubscriptionId,
                SubscriptionPlanType = type,
                PeriodStart = startDate,
                PeriodEnd = endDate,
                Status = isTrial ? SubscriptionStatus.ActiveTrial : SubscriptionStatus.ActivePayed,
            });
        }
        public bool CheckIfMember(Guid userId)
        {
            return memberships.Any(membership => membership.UserId == userId);
        }
    }
}
