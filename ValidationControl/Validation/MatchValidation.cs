using ValidationControl.Controls;

namespace ValidationControl.Validation
{
	public class MatchValidation : MatchValidationBase<BindableObject>
	{
	}

	public abstract class MatchValidationBase<T> : BaseValidation where T : BindableObject
	{
		public T TargetEntry { get; set; }

		protected override string DefaultMessage => "Values do not match";

		public override bool Validate(object value)
		{
			if (!(value is string text))
				return false;

			if (TargetEntry is MEntry mEntry)
				return text == mEntry.Text;
			if (TargetEntry is Entry entry)
				return text == entry.Text;

			return false;
		}
	}
}
