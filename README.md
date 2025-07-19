# MicroChassis
[![Build Status](https://github.com/microchassis/microchassis/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/microchassis/microchassis/actions?query=branch%3Amain)

MicroChassis is an opinionated implementation of the [Microservices Chassis Pattern](https://microservices.io/patterns/microservice-chassis.html) for .NET applications. Built for the enterprise it provides any organization with a standardized framework for structuring, preconfiguring and distributing dependencies that would usually handle cross-cutting concerns like logging, tracing, hosting etc. This would promote consistency, maintainability, and efficiency across all microservices while ensuring each service is evolved independently. It would also allow developers to focus on the unique aspects of their services without being bogged down by underlying infrastructure.

## Motivation

Microservices architecture has been adopted by a wide range of organizations from agile startups to modern enterprises and while multiple services offer great opportunities for flexibility, reliability and scalability, there are a number of practical challenges related to maintaining them. Meeting the  standards or implementing best practices in the organization can be quite a tedious task, even when same third-party or in-house components are involved in many services. The most recent or specific versions of those components should be used for security, licensing, regulatory or any other reasons, specific configurations matching existing policies and underlying infrastructure should be enforced and of course upgrading all of them should happen in a safe and reliable manner for each service. MicroChassis offers an intuitive way for packing, preconfiguring and distributing such components together using the .NET native packaging and dependency injection approaches.

## Getting started

### Concepts

As the name suggests MicroChassis is meant to be small and the few of its core components are located in the <code>MicroChassis</code> package. There we have the [MicroChassis](/src/MicroChassis/Core/MicroChassis.cs) base class managing a list of [IMicroModule](/src/MicroChassis/Abstractions/IMicroModule.cs) instances along with the [MicroChassisBuilder](/src/MicroChassis/Core/MicroChassisBuilder.cs) class that would allow for "mounting" additional ones. This is the main package and it's rarely referenced directly, but it's always there through one of the other two. The first one of them is the <code>MicroChassis.NETCore</code> package having the [HostBuilderMicroChassis](/src/MicroChassis.NETCore/Core/HostBuilderMicroChassis.cs) base class and the [IHostBuilderExtensions](/src/MicroChassis.NETCore/Extensions/IHostBuilderExtensions.cs) used with .NET Core applications. The other one is the <code>MicroChassis.AspNetCore</code> package containing the [WebApplicationBuilderMicroChassis](/src/MicroChassis.AspNetCore/Core/WebApplicationBuilderMicroChassis.cs) and [WebApplicationMicroChassis](/src/MicroChassis.AspNetCore/Core/WebApplicationMicroChassis.cs) base classes along with the [WebApplicationBuilderExtensions](/src/MicroChassis.AspNetCore/Extensions/WebApplicationBuilderExtensions.cs) and [WebApplicationExtensions](/src/MicroChassis.AspNetCore/Extensions/WebApplicationExtensions.cs) used with ASP.NET Core applications.

### Creating a MicroChassis

A MicroChassis is created to be shared within a company or a project and it would be good to be a separate package.

1. Create your own MicroChassis assembly that references one of the <code>MicroChassis.AspNetCore</code> or <code>MicroChassis.NETCore</code> packages based on the type of application you have. See the sample [csproj](/samples/Company.MicroChassis.AspNetCore/Company.MicroChassis.AspNetCore.csproj) file.

> [!TIP]
> A company-wide package would suggest a naming pattern like <code>CompanyName.MicroChassis.\*</code>, 
while a project specific one would have the project name included as well and be like <code>CompanyName.ProjectName.MicroChassis.\*</code> where CompanyName is the name of the company and ProjectName is the name of the project.

2. Create classes that inherit the provided <code>Application</code> and <code>Builder</code> ones and add the MicroModules you want to have predefined. See the sample [Application](/samples/Company.MicroChassis.AspNetCore/CompanyWebApplicationMicroChassis.cs) and [Builder](/samples/Company.MicroChassis.AspNetCore/CompanyWebApplicationBuilderMicroChassis.cs) classes.

### Creating a MicroModule

A MicroModule should only contain reference and setup logic making it light enough to reside within the MicroChassis assembly (see the [MicroModules](/samples/Company.MicroChassis.AspNetCore/MicroModules) folder) and that should be a common case but still, putting it in a separate package and referencing it could be a preferred option in other cases like the [Redis](/samples/Company.MicroModules.Redis/MicroModules) one.

1. Based on the functionality of the MicroModule:
    - Create a [MicroModules](/samples/Company.MicroChassis.AspNetCore/MicroModules) folder in the <ins>MicroChassis assembly</ins> when the functionality should be <ins>available in every</ins> microservice.
    - Create a [MicroModules](/samples/Company.MicroModules.Redis/MicroModules/) folder in a <ins>separate assembly</ins> when the functionality is <ins>exclusive for a service</ins>, but still repetitive within the company or project.
2. Create a class that implements the [IMicroModule](/src/MicroChassis/Abstractions/IMicroModule.cs) interface. See the sample [Hosting](/samples/Company.MicroChassis.AspNetCore/MicroModules/Hosting.cs), [Logging](/samples/Company.MicroChassis.AspNetCore/MicroModules/Logging.cs), [Metrics](/samples/Company.MicroChassis.AspNetCore/MicroModules/Metrics.cs), [Tracing](/samples/Company.MicroChassis.AspNetCore/MicroModules/Tracing.cs) and [Redis](/samples/Company.MicroModules.Redis/MicroModules/Redis.cs) classes.

### Using a MicroChassis

Once you have the MicroChassis with all of its MicroModules ready you just have to plug it into your application by calling the <code>.AddMicroChassis</code> and/or <code>.UseMicroChassis</code> extension methods during the initialization phase. See the sample [Program.cs](/samples/Company.Project.MicroService/Program.cs) file.

### Distribution

It is highly recommended to store your MicroChassis and MicroModules packages within a package management system where a strict access and versioning can be enforced. Of course that's not always the case with small teams or companies, then a shared location can be used instead.

## License

MicroChassis is licensed under the [MIT](LICENSE) license.