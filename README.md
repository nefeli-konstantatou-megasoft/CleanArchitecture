# Clean Architecture

## Design

This is a blog application meant to demonstrate Clean Architecture best practices, utilizing the following tools and practices:

- Project structure/Layering: The application is split into different layers, providing a loosely coupled, organized project structure.
- EF-Core + code-first migrations: Operations on entities are performed in a declarative manner, as well as minimizing the chances of development errors, which might easily arise when manually converting data between the fundamentally incompatible type systems of SQL and C#. 
- CQRS (Command Query Responsibility Segregation): There is a clear seperation of responsibilities between requests that mutate the state of our application (commands) and those that solely retrieve data (queries).
- Mediator pattern: The mediator is responsible for assigning requests to the right handler services, reducing code complexity, coupling between projects and the need of using the right services with the right DTOs (despite their relationship being trivial). 
- Result pattern: Errors are integrated into the state of responses, minimizing exception handling, as well as the performance overhead and control flow complexity it such handling introduces. 
- Mapster: For automtically creating mappings between objects based on names; albeit not being optimal for performance.
- Blazor: Control over the rendering method of individual pages is provided using Blazor (whether that be Static Server Server-Side Rendering, Interactive Server Rendering => WebSockets, Interactive WebAsssembly => WASM or Interactive Auto => WebSockets + WASM).
- Identity: The Identity tool is provides a customizable authentication and authorization system.

## Features

### User features

- Simple, to-the-point UI.
- Permissions based authorization system, featuring custom authorization components, attributes, claims and policies.
- Home page displaying the user's articles, allowing them to publish or edit each article.
- Articles view: A view of all of the existing articles.

### 'Moderator' features

- User management page, allowing moderators to edit the roles of each user.
- Roles management screen, allowing custom roles to be created, as well as the editing of already existing ones.
- Articles management screen, allowing moderators to manage articles per-user, utilizing a grid-based view.

Non-notable features include the login and registration pages, as well as the article editor.

## Requirements

- .NET SDK that supports .NET 10 or higher.
- Microsoft SQL Server.
- EF-Core tools.
- SDK support for ASP.NET Core as well as Blazor.

The project was verified to work on Microsoft Windows 11, as well as Arch Linux.
