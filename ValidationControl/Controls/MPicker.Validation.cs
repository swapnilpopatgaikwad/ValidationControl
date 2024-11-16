﻿using ValidationControl.Interface;

namespace ValidationControl.Controls
{
	public partial class MPicker : IValidatable
	{
		public List<IValidation> Validations { get; } = new();
		public bool IsValid { get => ValidationResults().All(x => x.isValid); }

		protected Lazy<Image> iconValidation = new Lazy<Image>(() => new Image
		{
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.End,
			Margin = new Thickness(0, 0, 5, 0),
		});

		protected Lazy<Label> labelValidation = new Lazy<Label>(() => new Label
		{
			HorizontalOptions = LayoutOptions.Start,
			Margin = new Thickness(5, 0, 0, 0),
		});


		protected bool lastValidationState = true;

		protected virtual void CheckAndShowValidations()
		{
			var results = ValidationResults().ToArray();
			var isValidationPassed = results.All(a => a.isValid);

			var isStateChanged = isValidationPassed != lastValidationState;

			lastValidationState = isValidationPassed;

			if (isValidationPassed)
			{
				if (isStateChanged)
				{
					errorIconsContainer.Remove(iconValidation.Value);
					_content.Remove(errorIconsContainer);
					this.Remove(labelValidation.Value);
					OnPropertyChanged(nameof(IsValid));
				}
			}
			else
			{
				var message = string.Join('\n', results.Where(x => !x.isValid).Select(s => s.message));
				labelValidation.Value.Text = message;

				if (isStateChanged)
				{
					errorIconsContainer.Add(iconValidation.Value);
					_content.Add(errorIconsContainer, 1, 0);
					this.Add(labelValidation.Value, row: 1);
					OnPropertyChanged(nameof(IsValid));
				}
			}
		}

		public virtual void ShowValidation()
		{
			CheckAndShowValidations();
		}

		public virtual void ResetValidation()
		{
			errorIconsContainer.Remove(iconValidation.Value);
			this.Remove(labelValidation.Value);
			lastValidationState = true;
		}

		protected IEnumerable<(bool isValid, string message)> ValidationResults()
		{
			foreach (var validation in Validations)
			{
				yield return ValidateOne(validation);
			}
		}

		protected (bool isValid, string message) ValidateOne(IValidation validation)
		{
			try
			{
				var value = GetValueForValidator();
				var validated = validation.Validate(value);
				return new(validated, validation.Message);
			}
			catch (Exception ex)
			{
				return new(false, ex.Message);
			}
		}

		protected virtual object GetValueForValidator()
		{
			return _picker.SelectedItem;
		}
	}
}
