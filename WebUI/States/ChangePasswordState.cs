namespace WebUI.States
{
	public class ChangePasswordState
	{
		public string? PasswordType = "password";
		public bool PasswordState = false;
		public string DisplayText = "Показать";
		public Action? Changed;

		public void ChangePasswordType()
		{
			PasswordState = !PasswordState;
			if (!PasswordState)
			{
				PasswordType = "text";
				DisplayText = "Скрыть";				
			}
			else
			{
				PasswordType = "password";
				DisplayText = "Показать";				
			}
			Changed?.Invoke();
		}
	}
}
