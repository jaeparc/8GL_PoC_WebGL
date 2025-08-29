using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    const string LAST_EMAIL_KEY = "LAST_EMAIL", LAST_PASSWORD_KEY = "LAST_PASSWORD";

    [Header("--- PANELS ---")]
    public GameObject ChoicePanel;
    public GameObject RegisterPanel;
    public GameObject LoginPanel;
    public GameObject PlayPanel;

    [Header("--- INPUT FIELDS ---")]
    public InputField RegisterUsername;
    public InputField RegisterEmail;
    public InputField RegisterPassword;
    public InputField RegisterConfirmPassword;
    public InputField LoginEmail;
    public InputField LoginPassword;
    [Header("--- LABELS ---")]
    public TextMeshProUGUI RegisterFeedback;
    public TextMeshProUGUI LoginFeedback;

    void Start()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            ChoicePanel.SetActive(false);
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(false);
            PlayPanel.SetActive(true);
        }
        else
        {
            ChoicePanel.SetActive(true);
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(false);
            PlayPanel.SetActive(false);
        }
        LoginFeedback.text = "";
        RegisterFeedback.text = "";
    }

    public void OnPressedLogin()
    {
        if(LoginEmail.text == "" || LoginPassword.text == "")
        {
            LoginFeedback.text = "Both email and password are required";
            return;
        }
        else if (!LoginEmail.text.Contains("@") || !LoginEmail.text.Contains("."))
        {
            LoginFeedback.text = "Please enter a valid email address";
            return;
        }
        else
            Login(LoginEmail.text, LoginPassword.text, false);
    }

    public void OnPressedRegister()
    {
        if(RegisterUsername.text == "" || RegisterEmail.text == "" || RegisterPassword.text == "")
        {
            RegisterFeedback.text = "All fields are required for registration";
            return;
        }
        else if (RegisterPassword.text.Length < 6)
        {
            RegisterFeedback.text = "Password must be at least 6 characters long";
            return;
        }
        else if (!RegisterEmail.text.Contains("@") || !RegisterEmail.text.Contains("."))
        {
            RegisterFeedback.text = "Please enter a valid email address";
            return;
        }
        else if (RegisterPassword.text != RegisterConfirmPassword.text)
        {
            RegisterFeedback.text = "Passwords do not match";
            return;
        }
        else
            Register(RegisterUsername.text, RegisterEmail.text, RegisterPassword.text);
    }

    private void Login(string email, string password, bool fromRegistration)
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        },
        successResult => {
            PlayerPrefs.SetString(LAST_EMAIL_KEY, email);
            PlayerPrefs.SetString(LAST_PASSWORD_KEY, password);
            PlayerPrefs.SetString("Username", successResult.InfoResultPayload.PlayerProfile.DisplayName);
            Debug.Log("Login success");
            if (fromRegistration)
                OnRegisterSuccess();
            else
                OnLoginSuccess();
        }, PlayfabFailure);
    }

    private void Register(string username, string email, string password)
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false
        },
        successResult => { Debug.Log("Register success"); Login(email, password, true); }, PlayfabFailure);
    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString("Username");
    }

    public void OnChooseLogin()
    {
        ChoicePanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void OnChooseRegister()
    {
        ChoicePanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnBackToChoice()
    {
        ChoicePanel.SetActive(true);
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(false);
    }

    public void OnLoginSuccess()
    {
        LoginPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }

    public void OnRegisterSuccess()
    {
        RegisterPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }
    
    public void PlayfabFailure(PlayFabError error)
    {
        Debug.LogError("PlayFab error: " + error.GenerateErrorReport());
    }
}
