using System.Collections.Generic;
using BlazorServer.Models;
using TanvirArjel.Blazor.Components;

namespace BlazorServer.Pages
{
    public partial class CreateEmployeeComponent
    {
        private EmployeeModel EmployeeModel { get; set; }

        private CustomValidator CustomValidator { get; set; }


        protected override void OnInitialized()
        {
            EmployeeModel = new EmployeeModel();
        }

        private void HanldeValidSummit()
        {
            List<string> errors = new List<string> { "The custom error.", "The custom error2." };
            CustomValidator.AddErrors(errors);
            CustomValidator.AddError("Name", "The name is required.");
            CustomValidator.DisplayErrors();
        }
    }
}
