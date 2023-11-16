using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public Transform ChatContent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        { 
            // 채팅을 입력한 후에도 이어서 입력할 수 있도록 설정
            input.ActivateInputField();

            if (input.text.Length == 0) return;
            string chat = PhotonNetwork.NickName + ":" + input.text;
            photonView.RPC("Chatting", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    void Chatting(string msg)
    {
        // ChatPrefab을 하나 만들어서 text에 값을 설정한다.
        GameObject chat = Instantiate(Resources.Load<GameObject>("String"));
        chat.GetComponent<Text>().text = msg;

        // 스크롤 뷰 - content에 지식을 등록한다.
        chat.transform.SetParent(ChatContent);

        // 채팅을 입력한 후에도 이어서 입력할 수 있도록 설정
        input.ActivateInputField();

        // input 텍스트를 초기화한다.
        input.text = "";
    }
}
