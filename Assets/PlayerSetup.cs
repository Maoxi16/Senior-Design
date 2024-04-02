using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
    public Movement movement;
    public GameObject camera;
   
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
}
