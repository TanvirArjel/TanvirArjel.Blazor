// <copyright file="EditContextExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using TanvirArjel.Blazor.Utilities;

namespace TanvirArjel.Blazor.Extensions
{
    /// <summary>
    /// Contain <see cref="EditContext"/> extension methods.
    /// </summary>
    public static class EditContextExtensions
    {
        /// <summary>
        /// Add <see cref="BootstrapValidationClassProvider"/> to <see cref="EditContext"/> to override blazor's default validation classes.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/> to which validation classes will be applied.</param>
        public static void AddBootstrapValidationClassProvider(this EditContext editContext)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            editContext.SetFieldCssClassProvider(new BootstrapValidationClassProvider());
        }

        /// <summary>
        /// Get the validation class for the input label based on the input field validation state.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/> of the input field.</param>
        /// <param name="fieldName">The input field name.</param>
        /// <returns>Returns "text-success" or "text-danger" based on the input field's validation state.</returns>
        public static string GetValidationCssClassForLabel(this EditContext editContext, string fieldName)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            FieldIdentifier fieldIdentifier = new FieldIdentifier(editContext.Model, fieldName);

            bool isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

            if (editContext.IsModified(fieldIdentifier))
            {
                return isValid ? "text-success" : "text-danger";
            }

            return isValid ? string.Empty : "text-danger";
        }
    }
}
