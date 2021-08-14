# TanvirArjel.Blazor
This library is extending ASP.NET Core Blazor functionalities to ease most common tasks for the developers. Currently, it has the following functionalities:

1. Adding constructor dependency injection support for the Blazor Components.
2. Useful extension methods on **NavigationManager** to handle query strings.
3. A pagination component to display pagination UI in Blazor Components.
4. A **CustomValidationMessages** component for adding custom error/validation messages to the EditConext model.
5. Extension method for adding bootstrap validation classes support in EditContext.

## ⭐ Giving a star

**If you find this library useful, please don't forget to encouraging me to do such more stuffs by giving a star to this repository. Thank you.**

## ✈️ How to get started
First install the `TanvirArjel.Blazor` [nuget](https://www.nuget.org/packages/TanvirArjel.Blazor/) package into your Blazor app as follows:

**PMC:**

     Install-Package TanvirArjel.Blazor
     
**.NET CLI:**

     dotnet add package TanvirArjel.Blazor
    
## 🛠️ Usage:

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
2. **NavigationManager Extensions:**

   a) To get query string value:
   ```C#
   using TanvirArjel.Blazor.Extensions;
   
   string tagName = _navigationManager.GetQuery("tag");
   int pageIndex = _navigationManager.GetQuery<int>("pageIndex");
   ```
   
   b) SetQuery() - 2 Overloads
   
   c) NavigateTo() - 3 Overloads

3. **For Pagination**:

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
                ListPath = "products"
            };
        }
   }
   ```
   *Note: Pagination styling depends on [Bootstrap](https://getbootstrap.com/) css framework.*
   
   
 4. **CustomValidationMessages** component for adding custom validation message to the EditContext as follows:

    In `.razor` file:

    ```C#
    <EditForm EditContext="FormContext" OnValidSubmit="HandleValidSubmit">
      <DataAnnotationsValidator />
      <CustomValidationMessages @ref="ValidationMessages" />
      <ValidationSummary Model="@LoginModel" />
      
      ......
    </EditForm>
    ```
  
    In `.razor.c`s file:
  
     ```C#
     private EditContext FormContext { get; set; }

     private LoginModel LoginModel { get; set; } = new LoginModel();

     private CustomValidationMessages ValidationMessages { get; set; }

     private async Task HandleValidSubmit()
     {
       try
       {
           HttpResponseMessage httpResponse = await _userService.LoginAsync(LoginModel);

           if (httpResponse.IsSuccessStatusCode)
           {
               JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
               {
                   PropertyNameCaseInsensitive = true
               };

               string responseString = await httpResponse.Content.ReadAsStringAsync();
               LoggedInUserInfo loginResponse = JsonSerializer.Deserialize<LoggedInUserInfo>(responseString, jsonSerializerOptions);

               if (loginResponse != null)
               {
                   await _hostAuthStateProvider.LogInAsync(loginResponse, "/");
               }
           }
           else
           {
               // This will automatically add all the server side validation messages to the EditContext model.
               await ValidationMessages.AddAndDisplayAsync(httpResponse);
               IsDisabled = false;
           }
       }
       catch (Exception exception)
       {
           // You cann also manually add error messages to the EditContext model.
           ValidationMessages.AddAndDisplay(ErrorMessage.ClientErrorMessage);
           await _exceptionLogger.LogAsync(exception);
       }
     }
     ```
  
   
 
