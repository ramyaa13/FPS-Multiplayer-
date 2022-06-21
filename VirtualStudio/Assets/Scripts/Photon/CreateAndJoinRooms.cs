using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System.Linq;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks 
{
    public static CreateAndJoinRooms Instance;

    public TMP_InputField CreateInput;
    public TMP_InputField JoinInput;

    [SerializeField] Transform RoomListContent;
    [SerializeField] GameObject RoomListItemPrefab;

    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;

    //(if control by host)
    [SerializeField] GameObject EnterGameButton;
    private MenuManager menuManager;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        menuManager = GetComponent<MenuManager>();
    }
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(CreateInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(CreateInput.text);
    }

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(CreateInput.text))
        {
            return;
        }
        PhotonNetwork.JoinRoom(JoinInput.text);
       
    }

    public void JoinOnFindRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }


    public override void OnJoinedRoom()
    {
        // PhotonNetwork.LoadLevel(2);
        menuManager.OpenPanel(3);
        menuManager.SetRoomname(PhotonNetwork.CurrentRoom.Name);

        foreach (Transform trans in PlayerListContent)
        {
            Destroy(trans.gameObject);
        }
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Count(); i++)
        {
           
            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
        }

        //(if control by host)
        EnterGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    //(if host leaves and automatically host switch to another player)
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        EnterGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }


    public void EnterGame()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Room Left");
        SceneManager.LoadScene(0);
       // menuManager.OpenPanel(0);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        menuManager.OpenPanel(4);
        menuManager.SetError(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //clear list every time update
        foreach(Transform trans in RoomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;

            Instantiate(RoomListItemPrefab, RoomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }
}
