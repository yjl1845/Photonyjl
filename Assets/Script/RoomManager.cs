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

    public void OnClickCreateRoom()
    {
        // �� �ɼ��� �����Ѵ�.
        RoomOptions Room = new RoomOptions();

        // �ִ� �������� ���� �����Ѵ�.
        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        // ���� ���� ���θ� �����Ѵ�.
        Room.IsOpen = true;

        // �κ񿡼� �� ����� �����ų�� �����Ѵ�.
        Room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(RoomName.text, Room);
    }
    public void AllDeleteRoom()
    {
        // transform ������Ʈ�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ������ �õ��Ѵ�.
        foreach(Transform trans in RoomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �����Ѵ�.
            Destroy(trans.gameObject);
        }
    }

    // �ش� �κ� �� ����� ��������� ������ ȣ��(�߰�, ����, ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    void UpdateRoom(List<RoomInfo>roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                if (roomList[i].RemovedFromList)
                {
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }
}
