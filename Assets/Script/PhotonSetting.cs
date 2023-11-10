using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonSetting : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField userID;
    [SerializeField] InputField password;

    // delegate void callback();
    // 
    // private void Start()
    // {
    //     Function();
    // 
    //     callback function;
    // 
    //     function = delegate { Debug.Log("Lamda Function"); };
    // 
    //     function();
    // }
    // 
    // public void Function()
    // {
    //     Debug.Log("Function");
    // }

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, (PlayFabError error) => NotificationManager.NotificationWindow(error.ToString()));
    }

    public void SignUp()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = userID.text
        };

        PlayerPrefs.SetString("Name", userID.text);

        PlayFabClientAPI.RegisterPlayFabUser(request, (RegisterPlayFabUserResult result) => NotificationManager.NotificationWindow(result.ToString()), (PlayFabError error) => NotificationManager.NotificationWindow(error.ToString()));
    }
}
