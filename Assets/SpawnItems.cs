using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnItems : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform spawnPos;

    private void Start()
    {
        PhotonNetwork.Instantiate(itemPrefab.name, spawnPos.position, Quaternion.identity);
    }
}