using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform spawnPos;

    private void Start()
    {

    }

    public CharacterStats SpawnEnemy()
    {
        GameObject enemy = PhotonNetwork.InstantiateRoomObject(EnemyPrefab.name, spawnPos.position, Quaternion.identity);
        return enemy.GetComponent<CharacterStats>();
    }


}
