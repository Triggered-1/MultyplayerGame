using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField NickNameInput;
    public TextMeshProUGUI infoText;
    private RoomOptions options;

    private void Start()
    {
        options = new RoomOptions();
        options.MaxPlayers = 2;
    }

    public bool TrySetName()
    {
        string name = NickNameInput.text;
        if (string.IsNullOrWhiteSpace(name))
        {
            infoText.text = $"\"{NickNameInput.text}\" is not a valid name";
            return false;
        }
        PhotonNetwork.LocalPlayer.NickName = name;
        return true;
    }

    public void CreateRoom()
    {
        if (!TrySetName())
            return;
        PhotonNetwork.CreateRoom(createInput.text,options);
    }

    public void JoinRoom()
    {
        if (!TrySetName())
            return;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        infoText.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        infoText.text = message;
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("StartScene");
    }
}
