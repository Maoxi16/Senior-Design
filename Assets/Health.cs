using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public bool islocalPlayer;
    [Header("UI")]
    public TextMeshProUGUI healthText;
    
   
    [PunRPC]
    public void TakeDamage(int _damage){
        health -= _damage;
        healthText.text = health.ToString();

        if(health  <= 0)
        {   
            if(islocalPlayer)
                RoomManager.instance.spawnPlayer();

            Destroy(gameObject);
        }

    }
}
