using ValidationControl.Controls;

namespace ValidationControl.Validation
{
	public class MatchValidation<T> : BaseValidation where T : BindableObject
	{
		public T TargetEntry { get; set; }  // T can be either MEntry or Entry

		protected override string DefaultMessage => "Values do not match";

		public override bool Validate(object value)
		{
			if (value is not string confirmPassword)
				return false;

			if (TargetEntry is MEntry mEntry)
			{
				return confirmPassword == mEntry.Text;  // For MEntry
			}
			else if (TargetEntry is Entry entry)
			{
				return confirmPassword == entry.Text;  // For normal Entry
			}
			return false;
		}
	}
}
