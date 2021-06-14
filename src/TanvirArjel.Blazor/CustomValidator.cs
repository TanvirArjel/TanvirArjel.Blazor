// <copyright file="CustomValidator.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TanvirArjel.Blazor
{
    /// <summary>
    /// Contains methods for adding custom validation message to Blazor FormContext.
    /// </summary>
    public class CustomValidator : ComponentBase
    {
        private ValidationMessageStore _messageStore;

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        /// <summary>
        /// Add a generic error message to the current form context.
        /// </summary>
        /// <param name="errorMessage">The error message to be added.</param>
        public void AddError(string errorMessage)
        {
            _messageStore.Add(CurrentEditContext.Field(string.Empty), errorMessage);
        }

        /// <summary>
        /// Add a generic error message to the current form context and display.
        /// </summary>
        /// <param name="errorMessage">The error message to be added and displayed.</param>
        public void AddAndDisplayError(string errorMessage)
        {
            _messageStore.Add(CurrentEditContext.Field(string.Empty), errorMessage);
            CurrentEditContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// Add a key specified error message to the current form context.
        /// </summary>
        /// <param name="key">The key of the error message.</param>
        /// <param name="errorMessage">The message to be added.</param>
        public void AddError(string key, string errorMessage)
        {
            _messageStore.Add(CurrentEditContext.Field(key), errorMessage);
        }

        /// <summary>
        /// Add a key specified error message to the current form context and display.
        /// </summary>
        /// <param name="key">The key of the error message.</param>
        /// <param name="errorMessage">The message to be added and displayed.</param>
        public void AddAndDisplayError(string key, string errorMessage)
        {
            _messageStore.Add(CurrentEditContext.Field(key), errorMessage);
            CurrentEditContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// Add a collection of error messages to the current context and display.
        /// </summary>
        /// <param name="errors">The erros to be added and displayed.</param>
        public void AddAndDiplayErrors(IDictionary<string, List<string>> errors)
        {
            AddErrors(errors);
            CurrentEditContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// Add a collection of error messages to the current context.
        /// </summary>
        /// <param name="errors">The erros to be added.</param>
        public void AddErrors(IDictionary<string, List<string>> errors)
        {
            if (errors == null)
            {
                return;
            }

            foreach (KeyValuePair<string, List<string>> err in errors)
            {
                _messageStore.Add(CurrentEditContext.Field(err.Key), err.Value);
            }
        }

        /// <summary>
        /// Display all the errors of the current context.
        /// </summary>
        public void DisplayErrors()
        {
            CurrentEditContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// Clear all the errors of the current context.
        /// </summary>
        public void ClearErrors()
        {
            _messageStore.Clear();
            CurrentEditContext.NotifyValidationStateChanged();
        }

        /// <summary>
        /// Called when the CustomValidator component is initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CustomValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(CustomValidator)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            _messageStore = new ValidationMessageStore(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (s, e) =>
                _messageStore.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) =>
                _messageStore.Clear(e.FieldIdentifier);
        }
    }
}
