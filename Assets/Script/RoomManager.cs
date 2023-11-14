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

    // 룸 목록을 저장하기 위한 자료구조
    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    private void Update()
    {
        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
            RoomCreate.interactable = true;
        else
            RoomCreate.interactable = false;
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // RoomCatalog에 여러 개의 Value값이 들어가 있다면 RoomInfo에 넣어준다.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            // 룸을 생성
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // RoomContect의 하위 오브젝트로 설정한다.
            room.transform.SetParent(RoomContent);

            // 룸 정보를 입력한다.
            room.GetComponent<Infomation>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void OnClickCreateRoom()
    {
        // 룸 옵션을 설정한다.
        RoomOptions Room = new RoomOptions();

        // 최대 접속자의 수를 설정한다.
        Room.MaxPlayers = byte.Parse(RoomPerson.text);

        // 룸의 오픈 여부를 설정한다.
        Room.IsOpen = true;

        // 로비에서 룸 목록을 노출시킬지 설정한다.
        Room.IsVisible = true;

        // 룸을 생성하는 함수
        PhotonNetwork.CreateRoom(RoomName.text, Room);
    }
    public void AllDeleteRoom()
    {
        // transform 오브젝트에 있는 하위 오브젝트에 접근하여 전체 삭제를 시도한다.
        foreach(Transform trans in RoomContent)
        {
            // Transform이 가지고 있는 게임 오브젝트를 삭제한다.
            Destroy(trans.gameObject);
        }
    }

    // 해당 로비에 방 목록의 변경사항이 있으면 호출(추가, 삭제, 참가)
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
