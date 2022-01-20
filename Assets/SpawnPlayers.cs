using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPos;
    private GameObject myPlayer;

    private void Start()
    {
        myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, Quaternion.identity);
        myPlayer.name = PhotonNetwork.LocalPlayer.NickName;
        Debug.Log(myPlayer.name);
    }
}
