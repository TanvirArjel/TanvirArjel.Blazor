# TanvirArjel.Blazor
This library is extending ASP.NET Core Blazor functionalities to ease most common tasks for the developers. Currently, it has the following functionalities:

1. Adding constructor dependency injection support for the Blazor Components.
2. A pagination component to display pagination UI in Blazor Components.

## ⭐ Giving a star ⭐

**If you find this library useful, please don't forget to encouraging me to do such more stuffs by giving a star to this repository. Thank you.**

## ✈️ How to get started ✈️
First install the `TanvirArjel.Blazor` [nuget](https://www.nuget.org/packages/TanvirArjel.Blazor/) package into your Blazor app as follows:

**PMC:**

     Install-Package TanvirArjel.Blazor
     
**.NET CLI:**

     dotnet add package TanvirArjel.Blazor
    
## 🛠️ Usage: 🛠️

1. To enable **Constructor Dependency Injection** support for the Blazor Components:

    *Blazor Server:*
    ```C#
    using TanvirArjel.Blazor.DependencyInjection;
        
    services.AddComponents();
    ```
    
    *Blazor Web Assembly:*
    ```C#
    using TanvirArjel.Blazor.DependencyInjection;
        
    builder.Services.AddComponents();
    ```
2. For **Pagination**:

   Add `@using TanvirArjel.Blazor.Components` to the `_Imports.razor` file. and then:
   
   ```C#
   <Pagination PaginationModel="PaginationModel"/>
   @code {
        private PaginationModel PaginationModel { get; set; }
        protected override void OnInitialized()
        {
            PaginationModel = new PaginationModel()
            {
                PageIndex = 5,
                PageSize = 10,
                TotalItems = 300,
                ListUrl = "products"
            };
        }
   }
   ```
   *Note: Pagination styling depends on [Bootstrap](https://getbootstrap.com/) css framework.*
