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
            // ä���� �Է��� �Ŀ��� �̾ �Է��� �� �ֵ��� ����
            input.ActivateInputField();

            if (input.text.Length == 0) return;
            string chat = PhotonNetwork.NickName + ":" + input.text;
            photonView.RPC("Chatting", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    void Chatting(string msg)
    {
        // ChatPrefab�� �ϳ� ���� text�� ���� �����Ѵ�.
        GameObject chat = Instantiate(Resources.Load<GameObject>("String"));
        chat.GetComponent<Text>().text = msg;

        // ��ũ�� �� - content�� ������ ����Ѵ�.
        chat.transform.SetParent(ChatContent);

        // ä���� �Է��� �Ŀ��� �̾ �Է��� �� �ֵ��� ����
        input.ActivateInputField();

        // input �ؽ�Ʈ�� �ʱ�ȭ�Ѵ�.
        input.text = "";
    }
}
