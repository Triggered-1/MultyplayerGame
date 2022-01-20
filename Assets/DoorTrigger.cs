using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviourPun
{
    [SerializeField] private int maxPlayers;
    [SerializeField] private TextMeshPro waitingText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers && collision.CompareTag("Player"))
        {
            photonView.RPC("LoadDungeonSceneRPC",RpcTarget.MasterClient);
        }
        else
        {
            waitingText.text = "Need One more Player to Start";
        }
    }

    [PunRPC]
    private void LoadDungeonSceneRPC()
    {
        //PhotonNetwork.LoadLevel("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }
}
