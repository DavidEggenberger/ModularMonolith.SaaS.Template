[![Build and Test](https://github.com/DavidEggenberger/ASPNETCore.Blazor.ModularMonolith.Template/actions/workflows/Build_Test.yml/badge.svg)](https://github.com/DavidEggenberger/ASPNETCore.Blazor.ModularMonolith.Template/actions/workflows/Build_Test.yml)

# Modular Monolith Template

Starter Template for building modular monolithic SaaS applications with ASP.NET Core, Blazor and EF Core.

## Architecture

The solution follows a "modular" architectural approach. The idea is, that every "Subdomain" is organised in an own module. 

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/ArchitectureOverview.png" />

### Projects Overview

**Shared.Kernel**: Extension methods, interfaces, endpoint constants and BuildingBlocks that can be used by any other project. <br/>
**Shared.DomainFeatures**: Infrastructure components (e.g. EF Core) that are used by the DomainFeatures of the Modules.<br/> 
**Web.Server**: Serves the WebAssembly client and the controllers that are defined in the Modules.<br/>
**Web.Client**: The WebAssembly client application. Its pages render the components defined in the Modules.<br/>

### Module Overview

The **Web.Server** references the **{ModuleName.Modules.Server}** project of each Module. The there defined controllers/pages are then served by the **Web.Server**. Each Module (with the exception of **Modules.LandingPages**) follows the same setup. **Modules.LandingPages** is an exception because it only needs to serve the Razor Components for the LandingPage. The **Identity/Subscription** Modules consist of five projects.

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/ModuleOverview.png" />

**Client**: Razor Components that are served by **Web.Client**. <br/>
**Shared**: DTOs shared between **Client** and **Server**. <br/> 
**Server**: Controllers that dispatch the respective Command/Query. <br/>
**DomainFeatures**: Defines the Commands/Queries and the respective Command/QueryHandlers <br/>
**IntegrationEvents**: Project that can be referenced by other Modules. When the **DomainFeatures** publish an IntegrationEvent it can then be handled. This allows for cross Module communication.

Besides modularity the template also follows a very pragmatic approach. The infrastructure components defined in **Shared.DomainFeatures** are not abstracted behind an interface. Instead they are directly used by the **DomainFeatures** of the respective modules. 

