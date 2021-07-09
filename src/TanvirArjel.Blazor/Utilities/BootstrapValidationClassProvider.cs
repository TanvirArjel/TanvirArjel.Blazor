// <copyright file="BootstrapValidationClassProvider.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;

namespace TanvirArjel.Blazor.Utilities
{
    /// <summary>
    /// This will override the Blazor's default validation css classes.
    /// </summary>
    internal class BootstrapValidationClassProvider : FieldCssClassProvider
    {
        /// <summary>
        /// Returns the bootstrap validation classes based on the input field state.
        /// </summary>
        /// <param name="editContext">The <see cref="EditContext"/> to which the validation classes will be applied.</param>
        /// <param name="fieldIdentifier">The <see cref="FieldIdentifier"/> of the input field.</param>
        /// <returns>A <see cref="string"/> representation of the bootstrap validation class.</returns>
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            bool isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

            if (editContext.IsModified(fieldIdentifier))
            {
                return isValid ? "is-valid" : "is-invalid";
            }

            return isValid ? string.Empty : "is-invalid";
        }
    }
}
