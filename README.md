[![Build and Test](https://github.com/DavidEggenberger/ASPNETCore.Blazor.ModularMonolith.Template/actions/workflows/Build_Test.yml/badge.svg)](https://github.com/DavidEggenberger/ASPNETCore.Blazor.ModularMonolith.Template/actions/workflows/Build_Test.yml)

# Modular Monolith Template

Starter Template for building modular monolithic SaaS applications with ASP.NET Core, Blazor and EF Core (SQL Server).

## Architecture

The solution follows a "modular" architectural approach. The idea is, that every "Subdomain" is organised in an own module. A module itself consists of multiple related projects. Note: The arrows show the dependencies between the projects and the modules.  

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/ArchitectureOverview.png" />

### Shared/Web Projects Overview

**Shared.Kernel**: Extension methods, interfaces, endpoint constants and BuildingBlocks that can be used by any other project. <br/>
**Shared.Features**: Infrastructure components (e.g. EF Core) that are used by the DomainFeatures of the Modules.<br/> 
**Shared.Client**: Shared razor components (e.g. Modals) used by the Client projects of the Modules.<br/>
**Web.Server**: Serves the WebAssembly client and the controllers that are defined in the Modules.<br/>
**Web.Client**: The WebAssembly client application. Its pages render the components defined in the Modules.<br/>

### Module Overview

A Module is a logical boundery that defines a subdomain (e.g. TenantIdentity or Subscription) of the application. The goal is to isolate the respective models and logic from other subdomains to avoid confusion and coupling. A Module therefore represents a Bounded Context from Eric Evans book Domain Driven Design. The **Web.Server** references the **{ModuleName.Modules.Server}** project of each Module. The there defined controllers/pages are then served by the **Web.Server**. Each Module (with the exception of **Modules.LandingPages**) follows the same setup. **Modules.LandingPages** is an exception because it only needs to serve the Razor Components for the LandingPage. Both the **Identity** and **Subscription** Modules consist of five projects.

<img src="https://raw.githubusercontent.com/DavidEggenberger/ModularMonolith.SaaS.Template/main/Assets/ModuleOverview.png" />

**Client**:
<br/>The project type of the **Client** project is a Razor Class library. It contains the Razor Components of the Module with the Client Logic. The Razor Components are referenced and rendered by the **Web.Client** project. **Web.Client** defines the Razor pages (e.g. Tenant page) that then consist of the Components in the **Client** project of the respective Module. To me, having all the pages in the **Web.Client** project gives me a better overview over the application and its structure. Important: The **Client** project of a Module can also reference the **Shared** project of another Module. This allows the **Client** project to call the API endpoints of the other Modules. This form of coupling is definitely a trade-off, but in my experience the Client Components of a Module are very likely to depend on the API's of other Modules anyways. For example, the Components to manage a Tenant (defined in the Tenant Module) also show an overview over Subscription history (retrievable through the API/Server from the Subscriptions Module). Because for an intuitive UI the boundaries between the Modules Components are vague, I like the more pragmatic approach with the **Client** project of a Module being able to directly consume the API's of other Modules insted of dogmatically isolating the Client project to only call API's from the same Module. The routes of all API controllers are defined in the EndpointConstants file of the **Shared.Kernel** project. <br/>

**Shared**: 
<br/>DTOs that are shared between the **Server** and **Client** Modules. As explained in the previous section the **Shared** project can also be referenced by the **Client** project of other Modules which enables for the Client logic of a Module to call API's of another Module. <br/> 

**Server**: 
<br/>Contains the API Controllers of the Module. The Controllers handle the incoming Web Requests by transforming the received DTO into the respective Query/Command that is then dispatched. In the Server project also all the services needed for the Module are registered to the DI container.<br/>

**DomainFeatures**: 
<br/>Besides modularity the template also follows a very pragmatic approach. Instead of layering with a "Clean Architecture" structure, the template organizes its code in vertical slices. While the goal of the Clean Architecture is to organize code through layering by technicality (e.g. one Project for all Domain entities, one Project for all Services and one for all Repositories) the goal of a "Vertical Slice" architecture is to group the code by its business domain. This means that the entities (Domain layer), Command/QueryHandlers (Application layer) and Infrastructure Configuration (Infrastructure Layer) of a respective feature (e.g. Tenant Management) all reside in the same **DomainFeatures** project of a Module. Therefore the **DomainFeatures** project itself is the "Vertical Slice". Having all the files in the same project makes it easier to add changes as we no longer must switch between different projects and need to adhere to layering abstraction in between them. The **DomainFeatures** Modules of the TenantIdentity and Subscription Modules have both 3 top level folders, Aggregates, Application and Infrastructure.
The Aggregates folder holds all of the Module's Aggregates. An Aggregate is a Domain Driven Design Pattern to organize entities, Martin Fowler defines it as follows: "cluster of domain objects that can be treated as a single unit". Every Aggregate revolves around an AggregateRoot. Its used to logically structure the Aggregate through encapsulating child entities that can only be retrieved and updated through calling the AggregateRoot's methods. Both the AggregateRoot and its Child Entities are modelled following the principles of Domain Driven Design meaning that the entity both contains data and behaviour. Besides the entities each AggregateRoot also has a Commands and a Queries subfolder. They hold the Commands/Queries with their respective Command/QueryHandlers. The Commands are used to update the AggregateRoot with its Child Entities and the Queries to retrieve them.     
The application folder contains the IntegrationEventHandlers and the Commands/Queries together with their Command/QueryHandlers. Handling the IntegrationEvents that are published by the **Features** projects of other Modules enables cross Module communication without tightly coupling the Modules. The Commands/Queries in the Application folder involve more then one Aggregate.
The infrastructure folder contains all the needed Infrastructure components. Typically this includes the EF Core DbContext and Configuration objects.

**IntegrationEvents**: 
<br/>Defines the IntegrationEvents and is intended to be referenced by other Modules so that the published IntegrationEvents can be handled which enables cross Module communication.



