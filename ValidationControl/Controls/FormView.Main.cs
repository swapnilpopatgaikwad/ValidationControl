using ValidationControl.Interface;

namespace ValidationControl.Controls
{
	public partial class FormView : StackLayout
	{
		public Func<bool> SubmitAction => Submit;
		public Func<bool> ResetAction => Reset;

		/// <summary>
		/// Validates the form and shows validation messages for invalid fields if necessary.
		/// </summary>
		/// <returns>True if all validations pass, otherwise false.</returns>
		public virtual bool Submit()
		{
			bool isValid = ValidateChildren();

			if (!isValid)
			{
				ShowValidationMessages();
			}

			return isValid;
		}

		/// <summary>
		/// Resets the validation state for all children that implement IValidatable.
		/// </summary>
		/// <returns>Always returns true.</returns>
		public virtual bool Reset()
		{
			foreach (var child in GetValidatableChildren())
			{
				child.ResetValidation();
			}
			return true;
		}

		/// <summary>
		/// Checks if all child elements pass validation.
		/// </summary>
		/// <param name="view">The parent layout containing the children to validate.</param>
		/// <returns>True if all validations pass, otherwise false.</returns>
		public bool CheckValidation(Layout view)
		{
			return !GetValidatableChildren(view).Any(validatable => !validatable.IsValid);
		}

		/// <summary>
		/// Validates all child elements and returns the result.
		/// </summary>
		/// <returns>True if all children pass validation, otherwise false.</returns>
		private bool ValidateChildren()
		{
			return CheckValidation(this);
		}

		/// <summary>
		/// Shows validation messages for all invalid child elements.
		/// </summary>
		private void ShowValidationMessages()
		{
			foreach (var child in GetValidatableChildren())
			{
				child.ShowValidation();
			}
		}

		/// <summary>
		/// Recursively retrieves all children implementing IValidatable from a given layout.
		/// </summary>
		/// <param name="layout">The parent layout to search.</param>
		/// <returns>An enumerable of IValidatable children.</returns>
		private static IEnumerable<IValidatable> GetValidatableChildren(Layout layout)
		{
			if (layout == null)
				yield break;

			foreach (var child in layout.Children)
			{
				if (child is IValidatable validatable)
				{
					yield return validatable;
				}

				if (child is Layout nestedLayout)
				{
					foreach (var nestedChild in GetValidatableChildren(nestedLayout))
					{
						yield return nestedChild;
					}
				}
			}
		}

		/// <summary>
		/// Retrieves all children implementing IValidatable from the current layout.
		/// </summary>
		/// <returns>An enumerable of IValidatable children.</returns>
		private IEnumerable<IValidatable> GetValidatableChildren()
		{
			return GetValidatableChildren(this);
		}
	}
}
