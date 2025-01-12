using ValidationControl.Controls;

namespace ValidationControl.Validation
{
    public class DateValidation : DateValidationBase<BindableObject>
    {
    }

	public abstract class DateValidationBase<T> : BaseValidation where T : BindableObject
	{
		public T TargetEntry { get; set; }

		protected override string DefaultMessage => "Selected date is invalid.";

		public override bool Validate(object value)
		{
			if (!(value is DateTime selectedDate))
			{
				return false;
			}

			if (TargetEntry is MDatePicker datePicker && datePicker.Date is DateTime referenceDate)
			{
				// Validate that the selected date is after the reference date
				return selectedDate > referenceDate;
			}

			if (TargetEntry is DatePicker datePickerControl && datePickerControl.Date is DateTime referencePickerDate)
			{
				// Validate against DatePicker for general case
				return selectedDate > referencePickerDate;
			}

			return false;
		}
	}
}
