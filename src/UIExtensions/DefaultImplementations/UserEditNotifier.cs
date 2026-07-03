namespace UIFramework.UiExtensions;


/// <summary>
/// Default implementation of IUserEditedNotifier
/// Use this if you wanna be informed of edits made by the user that aren't applied to the Value property yet
/// </summary>
public class UserEditNotifier : DefaultValidator, IUserEditedNotifier
{
    ///<inheritdoc/>
    public Action<object> OnUserEdit { get; set; }
}