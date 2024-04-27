using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPun
{
    public Movement movement;
    public GameObject camera;
   
    public string nickname;

    public TextMeshPro nicknameText;
    void Start()
    {
        // Check if this is the local player
        if (photonView.IsMine)
        {
            ActivateLocalPlayer();
           
        }
        else
        {
            // Optionally deactivate or adjust components for remote players
            camera.SetActive(false);
        }
    }

    public void ActivateLocalPlayer()
    {
        Debug.Log("Activating camera for local player.");
        movement.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void setnickname(string _name){
        nickname = _name;
        nicknameText.text = nickname;
    }
}
