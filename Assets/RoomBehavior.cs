using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomBehavior : MonoBehaviourPun
{
    [SerializeField] private Animator[] doorAnimators;
    [SerializeField] private BoxCollider2D roomTrigger;
    [SerializeField] private EnemySpawner[] spawners;
    private int playersInRoom;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC(nameof(OpenDoorsRPC), RpcTarget.AllBuffered);
        enemyCount = spawners.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            photonView.RPC(nameof(EnterRoomRPC), RpcTarget.MasterClient);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>())
        {
            photonView.RPC(nameof(ExitRoomRPC), RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void EnterRoomRPC()
    {
        playersInRoom++;
        Debug.Log(playersInRoom);
        if (playersInRoom == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            photonView.RPC("CloseDoorsRPC", RpcTarget.AllBuffered);
            StartCoroutine(Spawn());
        }
    }
    [PunRPC]
    private void ExitRoomRPC()
    {
        playersInRoom--;
        Debug.Log(playersInRoom);
    }

    [PunRPC]
    private void OpenDoorsRPC()
    {
        foreach (Animator anim in doorAnimators)
        {
            anim.SetBool("doorIsOpen", true);
        }
    }

    [PunRPC]
    private void CloseDoorsRPC()
    {
        foreach (Animator anim in doorAnimators)
        {
            anim.SetBool("doorIsOpen", false);
        }
        Destroy(roomTrigger);
    }



    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.SpawnEnemy().onDeath += EnemyDied;
            Debug.Log("Method linked");
        }
    }

    private void EnemyDied()
    {
        enemyCount--;
        Debug.Log(enemyCount);
        if (enemyCount == 0)
        {
            Debug.Log("Alle ded");
            photonView.RPC(nameof(OpenDoorsRPC), RpcTarget.AllBuffered);
        }
    }
}
