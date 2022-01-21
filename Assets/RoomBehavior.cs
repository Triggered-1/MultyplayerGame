using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomBehavior : MonoBehaviourPun
{
    [SerializeField] private bool dontCloseDoors;
    [SerializeField] private bool isStartRoom;
    [SerializeField] private Animator[] doorAnimators;
    [SerializeField] private BoxCollider2D roomTrigger;
    [SerializeField] private EnemySpawner[] spawners;
    [SerializeField] private Collider2D boundingBox;
    private bool roomIsCleared;
    private int playersInRoom;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC(nameof(OpenDoorsRPC), RpcTarget.AllBuffered);
        enemyCount = spawners.Length;
        if (isStartRoom)
        {
            GameManager.instance.RoomCleared();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") )
        {
            collision.GetComponent<PlayerController>().ChangeCamConfiner(boundingBox);
            if (!roomIsCleared)
            {
                photonView.RPC(nameof(EnterRoomRPC), RpcTarget.MasterClient); 
            }
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
        if (playersInRoom == PhotonNetwork.CurrentRoom.PlayerCount && !dontCloseDoors)
        {
            photonView.RPC("CloseDoorsRPC", RpcTarget.AllBuffered);
            StartCoroutine(Spawn());
        }
    }
    [PunRPC]
    private void ExitRoomRPC()
    {
        playersInRoom--;
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
    }



    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.SpawnEnemy().onDeath += EnemyDied;
        }
    }

    private void EnemyDied()
    {
        enemyCount--;
        Debug.Log(enemyCount);
        if (enemyCount == 0)
        {
            photonView.RPC(nameof(OpenDoorsRPC), RpcTarget.AllBuffered);
            GameManager.instance.RoomCleared();
            roomIsCleared = true;
        }
    }
}
