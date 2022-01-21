using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private Animator doorAnim;
    public static GameManager instance { get; private set; }
    private int roomCount;
    private int clearedRooms;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetRoomsCount();
        doorAnim.SetBool("doorIsOpen", false);
    }

    public void GetRoomsCount()
    {
        RoomBehavior[] roomBehaviors = FindObjectsOfType<RoomBehavior>();
        if (roomBehaviors != null)
        {
            roomCount = roomBehaviors.Length; 
        }
    }

    public void RoomCleared()
    {
        clearedRooms++;
        if (clearedRooms == roomCount)
        {
            doorAnim.SetBool("doorIsOpen", true);
        }
    }
}
