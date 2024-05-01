using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public AudioListener myAudioListener;
    public GameObject Player;
    [Space]
    public Transform[] spawnpoints;

    [Space]

    public GameObject roomCam;
    // Start is called before the first frame update
    [Space]
    public GameObject nameUI;
    public GameObject connectionUI;
    public string nickname = "unnamed";

    

    public string roomNameToJoin = "test";
  [HideInInspector]

    public int kills = 0;
   [HideInInspector]
    public int deaths = 0;
    void Awake(){
        instance = this;
    }

    public  void ChangeNickName(string _name){
        nickname = _name;

    }
    public void JoinRoomButtonPressed(){
    Debug.Log("Button Pressed: Attempting to connect...");
    PhotonNetwork.ConnectUsingSettings();
    PhotonNetwork.JoinOrCreateRoom(roomNameToJoin,null,null);


    nameUI.SetActive(false);
    connectionUI.SetActive(true);
}

    // void Start()
    // {
    //     // Debug.Log(" Connect ...");

    //     // PhotonNetwork.ConnectUsingSettings();
        
    // }

    // public override void OnConnectedToMaster()
    // {
    //     base.OnConnectedToMaster();

    //     Debug.Log("Connected to Server");
        
    //     PhotonNetwork.JoinLobby();
    // }

    // public override void OnJoinedLobby()
    // {
    //     base.OnJoinedLobby();

        

    //     Debug.Log("We are in the lobby");
    //     PhotonNetwork.JoinOrCreateRoom(roomNameToJoin,null,null);


    // }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        roomCam.SetActive(false);
        Debug.Log("We are conect and in a room now");
       
        spawnPlayer();
        

    }

    public void spawnPlayer(){

         Transform spawnpoint = spawnpoints[UnityEngine.Random.Range(0,spawnpoints.Length)];
         GameObject _player = PhotonNetwork.Instantiate(Player.name,spawnpoint.position,Quaternion.identity);
    
         _player.GetComponent<Health>().islocalPlayer = true;

         _player.GetComponent<PhotonView>().RPC("setnickname",RpcTarget.AllBuffered,nickname);
         PhotonNetwork.LocalPlayer.NickName=nickname;

}
public void SetHash(){
        try{
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash["kills"]= kills;
            hash["deaths"]=deaths;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    catch{
        //do nothing
    }
}
}
