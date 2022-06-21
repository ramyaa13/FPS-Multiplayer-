using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomName;
    
    public RoomInfo roomInfo;

    public void Setup(RoomInfo info)
    {
        roomInfo = info;
        RoomName.text = info.Name;
    }

    public void RoomClick()
    {
        CreateAndJoinRooms.Instance.JoinOnFindRoom(roomInfo);
    }
}
