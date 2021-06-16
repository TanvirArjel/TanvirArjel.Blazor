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
            List<string> errors = new List<string> { null, null };
            CustomValidator.AddErrors(errors);
            CustomValidator.AddError("Name", null);
            CustomValidator.DisplayErrors();
        }
    }
}
