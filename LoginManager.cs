//using ElRaccoone.Tweens;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private InputField email;
    [SerializeField] private InputField password;
    [SerializeField] private Text messageText;
    [Space]
    [SerializeField] private GameObject login;
    [SerializeField] private GameObject play;

    private bool showPassword;

    internal static string sessionTicket;
    internal static string playerId;
    internal static string playerName;

    private void Awake()
    {
        //login.TweenAnchoredPositionY(Screen.height, 0);
        //play.TweenAnchoredPositionY(-Screen.height, 0);

        LoginOpen();

        Load();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("email"))
        {
            email.text = PlayerPrefs.GetString("email");
            password.text = PlayerPrefs.GetString("password");
        }
    }

    private void Save()
    {
        PlayerPrefs.SetString("email", email.text);
        PlayerPrefs.SetString("password", password.text);
    }

    #region Register
    public void OnRegister()
    {
        if (password.text.Length < 6)
        {
            messageText.text = "Enter password with a minimum of 6 characters";
            return;
        }

        var request = new RegisterPlayFabUserRequest { Email = email.text, Password = password.text, RequireBothUsernameAndEmail = false };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        //create data

        messageText.text = "Register successful";
        Debug.Log("Register successful");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        OnError(error);
    }
    #endregion

    #region Login
    public void OnLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = email.text, Password = password.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        sessionTicket = result.SessionTicket;
        playerId = result.PlayFabId;

        //Get data

        PlayOpen();
        Save();

        messageText.text = "Login successful";
        Debug.Log("Login successful");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        OnError(error);
    }
    #endregion

    #region UI
    public void TogglePassword()
    {
        if (!showPassword)
        {
            showPassword = true;
            password.contentType = InputField.ContentType.Standard;
        }
        else if (showPassword)
        {
            showPassword = false;
            password.contentType = InputField.ContentType.Password;
        }

        password.ForceLabelUpdate();
    }

    public void LoginOpen()
    {
        //login.TweenAnchoredPositionY(0, 1).SetEaseCircInOut();
    }

    public void LoginClose()
    {
        //login.TweenAnchoredPositionY(Screen.height, 1).SetEaseCircInOut();

        showPassword = false;
        password.contentType = InputField.ContentType.Password;
        password.ForceLabelUpdate();
    }

    public void PlayOpen()
    {
        //play.TweenAnchoredPositionY(0, 1).SetEaseCircInOut();
    }

    public void PlayClose()
    {
        //play.TweenAnchoredPositionY(-Screen.height, 1).SetEaseCircInOut();
    }
    #endregion

    #region Rest Password
    public void RestPassword()
    {
        PlayFabClientAPI.SendAccountRecoveryEmail(new SendAccountRecoveryEmailRequest
        {
            Email = email.text,
            TitleId = "7E691"
        }, result =>
        {
            OnPasswordReset(result);
        }, error =>
        {
            OnError(error);
        });
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset email sent";
        Debug.Log("Password reset email sent");
    }
    #endregion

    private void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
