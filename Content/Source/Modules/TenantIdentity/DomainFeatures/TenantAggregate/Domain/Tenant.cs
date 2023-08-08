using Modules.TenantIdentity.DomainFeatures.Domain.Exceptions;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.DomainKernel;
using Shared.Infrastructure.DomainKernel.Attributes;
using Shared.Infrastructure.DomainKernel.Exceptions;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
{
    [AggregateRoot]
    public class Tenant : Entity
    {
        private readonly IExecutionContextAccessor executionContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        private Tenant() { }
        public Tenant(TenantIdentityDbContext tenantIdentityDbContext, IExecutionContextAccessor executionContextAccessor)
        {
            _authorizationService = tenantIdentityDbContext.AuthorizationService;
            this.executionContextAccessor = executionContextAccessor;
        }

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

        public static async Task<Tenant> CreateTenantWithAdminAsync(string name, Guid adminUserId)
        {
            return new Tenant
            {


            };
        }

        public void AddUser(Guid userId, TenantRole role)
        {
            _authorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

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
            _authorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }
        }

        public void RemoveUser(Guid userId)
        {
            _authorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

            if (CheckIfMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

            memberships.Remove(memberships.Single(m => m.UserId == userId));
        }

        public void InviteUserToRole(Guid userId, TenantRole role)
        {
            _authorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);

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

        public async void DeleteTenantMembership(Guid membershipId)
        {
            var tenantMembership = Memberships.SingleOrDefault(t => t.Id == membershipId);
            if(tenantMembership == null)
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
            _authorizationService.ThrowIfUserIsNotInRole(TenantRole.Admin);
        }

        public TenantDTO ToDTO() => new TenantDTO();
        public TenantDetailDTO ToDetailDTO() => new TenantDetailDTO();

    }
}
