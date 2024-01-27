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

The **Web.Server** references the **{ModuleName.Modules.Server}** project of each Module. The there defined controllers/pages are then served by the **Web.Server**. Each Module (with the exception of **Modules.LandingPages**) follows the same setup. **Modules.LandingPages** is an exception because it only needs to serve the Razor Components for the LandingPage. Both the **Identity** and **Subscription** Modules consist of five projects.

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/ModuleOverview.png" />

**Client**:
<br/>Contains the Client logic and the Razor Components. The Razor Components are referenced and rendered by the **Web.Client** project. <br/>

**Shared**: 
<br/> DTOs that are shared between the **Server** and **Client** Modules. <br/> 

**Server**: 
<br/>The API Controllers that dispatch the respective Command/Query. In the Server project also all the services for the Module are registered. <br/>

**Features**: 
<br/>The "Vertical Slice" containing the Domain, Application and Infrastructure logic. The application logic can publish IntegrationEvents from **IntegrationEvents**.<br/>

**IntegrationEvents**: 
<br/>Defines the IntegrationEvents and is intended to be referenced by other Modules so that the published IntegrationEvents can be handled which enables cross Module communication.

Besides modularity the template also follows a very pragmatic approach. Instead of relying on layering with a "Clean Architecture" structure the template organizes its code in vertical slices. This means that the entities (Domain layer), Command/QueryHandlers (Application layer) and Infrastructure Configuration (Infrastructure Layer) all reside in the same **Features** project of a Module.  

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/FeaturesOverview.png" />

**Aggregates**: Domain Driven Design Pattern to organize entities: "cluster of domain objects that can be treated as a single unit (Martin Fowler)"<br/>
 **TenantAggregate**: <br/>
  **Application**: Contains the application logic split into Commands, Queries and IntegrationEventHandlers<br/> 
   **Commands**: The Commands with their respective CommandHandlers <br/>
   **IntegrationEventHandlers**: IntegrationEventHandlers handling IntegationEvents published from other Modules<br/>
   **Queries**: The Queries with their respective QueryHandlers <br/>
  **Domain**: The aggregate's entities moduled following the principles of Domain Driven Design <br/> 
 **Infrastructure**: EF Core DbContext and Configuration for the whole Module



