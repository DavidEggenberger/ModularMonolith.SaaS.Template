using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions;
using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Domain;
using Shared.Features.Errors;
using Shared.Kernel.DomainKernel;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain
{
    public class Tenant : Entity
    {
        public string Name { get; private set; }
        public SubscriptionPlanType SubscriptionPlan { get; private set; }
        public List<TenantMembership> Memberships { get; private set; }
        public List<TenantInvitation> Invitations { get; private set; }

        public static Tenant CreateTenant(string tenantName, Guid adminUserId)
        {
            return new Tenant
            {
                Name = tenantName,
                Memberships = new List<TenantMembership>
                {
                    new TenantMembership(adminUserId, TenantRole.Admin)
                }
            };
        }

        public void AddUser(Guid userId, TenantRole role)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            TenantMembership tenantMembership;
            if ((tenantMembership = Memberships.SingleOrDefault(m => m.UserId == userId)) is not null)
            {
                throw Error.DomainException("User is already a member of the tenant", StatusCodes.Status409Conflict);
            }
            else
            {
                Memberships.Add(new TenantMembership(userId, role));
            }
        }

        public void ChangeRoleOfTenantMember(Guid userId, TenantRole newRole)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfUserIsMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

            TenantMembership tenantMembership = Memberships.Single(m => m.UserId == userId);
            tenantMembership.UpdateRole(newRole);
        }

        public void RemoveUser(Guid userId)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (CheckIfUserIsMember(userId) is false)
            {
                throw new MemberNotFoundException();
            }

            var membership = Memberships.Single(m => m.UserId == userId);
            if (membership.Role == TenantRole.Admin && Memberships.Count(m => m.Role == TenantRole.Admin) == 1)
            {
                throw Error.DomainException("Cant remove only admin from tenant", StatusCodes.Status409Conflict);
            }

            Memberships.Remove(Memberships.Single(m => m.UserId == userId));
        }

        public void InviteUserToRole(string email, TenantRole role)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            if (Invitations.Any(invitation => invitation.Email == email))
            {
                throw Error.DomainException("User is already invited", StatusCodes.Status409Conflict);
            }

            Invitations.Add(TenantInvitation.Create(this, email, role));
        }

        public void DeleteTenantMembership(Guid membershipId)
        {
            ThrowIfCallerIsNotInRole(TenantRole.Admin);

            var tenantMembership = Memberships.SingleOrDefault(t => t.Id == membershipId);
            if (tenantMembership == null)
            {
                throw Error.NotFound(nameof(TenantMembership), membershipId);
            }

            Memberships.Remove(tenantMembership);
        }

        public bool CheckIfUserIsMember(Guid userId)
        {
            return Memberships.Any(membership => membership.UserId == userId);
        }

        public void UpdateSubscriptionPlan(SubscriptionPlanType subscriptionPlanType)
        {
            SubscriptionPlan = subscriptionPlanType;
        }


        public TenantDTO ToDTO() => new TenantDTO();
        public TenantExtendedDTO ToDetailDTO() => new TenantExtendedDTO();

    }

    public class TenantEFConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant");
        }
    }
}
