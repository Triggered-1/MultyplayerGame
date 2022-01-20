using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManger : MonoBehaviourPun
{
    private void Awake()
    {
        //PhotonNetwork.Instantiate("Player", this.transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        //photonView.RPC("SpawnEnemy", RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
