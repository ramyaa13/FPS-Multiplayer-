using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerGameManager : MonoBehaviour
{
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(view.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerObject"), randomPosition, Quaternion.identity);
    }
}
