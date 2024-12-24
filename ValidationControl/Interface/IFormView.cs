namespace ValidationControl.Interface
{
	public interface IFormView
	{
		Func<bool> SubmitAction { get; }
		Func<bool> ResetAction { get; }

		bool Submit() { return true; }
		bool Reset() { return true; }
	}
}
