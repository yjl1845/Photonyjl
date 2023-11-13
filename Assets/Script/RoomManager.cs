using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button RoomCreate;
    public InputField RoomName;
    public InputField RoomPerson;
    public Transform RoomContent;

    // �� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    private void Update()
    {
        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
            RoomCreate.interactable = true;
        else
            RoomCreate.interactable = false;
    }

    // �뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // RoomCatalog�� ���� ���� Value���� �� �ִٸ� RoomInfo�� �־��ش�.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            // ���� ����
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContect�� ���� ������Ʈ�� �����Ѵ�.
            room.transform.SetParent(RoomContent);

            // �� ������ �Է��Ѵ�.
            room.GetComponent<Infomation>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
