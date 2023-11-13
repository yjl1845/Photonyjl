using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Infomation : MonoBehaviourPunCallbacks
{
    public Text roomData;
    private string roomName;

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetInfo(string Name, int Current, int Max)
    {
        roomName = Name;
        roomData.text = Name + "(" + Current + "/" + Max + ")";
    }
}
