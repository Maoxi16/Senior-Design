using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public AudioListener myAudioListener;
    public GameObject Player;
    [Space]
    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(" Connect ...");

        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        

        Debug.Log("We are in the lobby");
        PhotonNetwork.JoinOrCreateRoom("test",null,null);


    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are conect and in a room now");
        GameObject _player = PhotonNetwork.Instantiate(Player.name,spawnpoint.position,Quaternion.identity);

        // _player.GetComponent<PlayerSetup>().IsLocalPlayer();

    }









}
