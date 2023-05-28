using Shared.Modules.Layers.Infrastructure.Identity.Commands;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using Modules.IdentityModule.Domain;
using Modules.TenantIdentityModule.Layers.Infrastructure;

namespace Application.Shared.Modules.Layers.Infrastructure.Identity.CommandHandler
{
    //public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    //{
    //    //private readonly SignInManager<ApplicationUser> signInManager;
    //    //private readonly ApplicationUserManager userManager;
    //    //private readonly ICommandDispatcher commandDispatcher;
    //    ////private readonly IStripeCustomerService stripeCustomerService;
    //    //public CreateUserCommandHandler(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager, ICommandDispatcher commandDispatcher, IStripeCustomerService stripeCustomerService)
    //    //{
    //    //    this.signInManager = signInManager;
    //    //    this.userManager = userManager;
    //    //    this.commandDispatcher = commandDispatcher;
    //    //    //this.stripeCustomerService = stripeCustomerService;
    //    //}
    //    //public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
    //    //{
    //    //    var result = await userManager.CreateAsync(command.User);

    //    //    if (result.Succeeded)
    //    //    {
    //    //        await userManager.AddLoginAsync(command.User, command.LoginInfo);
    //    //        await signInManager.SignInAsync(command.User, isPersistent: false, command.LoginInfo.LoginProvider);

    //    //        //var stripeCustomer = await stripeCustomerService.CreateStripeCustomerAsync(command.User.UserName, command.User.Email);
    //    //        //command.User.StripeCustomerId = stripeCustomer.Id;

    //    //        //await userManager.UpdateAsync(command.User);
    //    //    }
    //    //}
    //}
}
