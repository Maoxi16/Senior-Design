using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class Weapon : MonoBehaviour
{
    public int damage;
    public float firerate;
    public Camera camera;
    [Header("VFX")]
    public GameObject hitVFX;
    private float nextFire;

    [Header("Amo")]
    public int mag = 5;
    public int amo = 30;
    public int magAmo =30;
    [Header("UI")]

    public TextMeshProUGUI magText;
    public TextMeshProUGUI amoText;

    [Header("animation")]
    public Animation animation;
    public AnimationClip reload;
    void start(){

        magText.text = mag.ToString();
        amoText.text = amo + "/" +magAmo;
    }
    void Update()
    {
        if(nextFire > 0)
            nextFire -= Time.deltaTime;

        if(Input.GetButton("Fire1")&& nextFire <=0 && amo >0 && animation.isPlaying == false ){
            nextFire =1 /firerate;
            amo --;

            magText.text = mag.ToString();
            amoText.text = amo + "/" +magAmo;
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
        }
    }


    void Reload(){

        animation.Play(reload.name);
        if (mag >0){
            mag --;

            amo = magAmo;

        }
        magText.text = mag.ToString();
        amoText.text = amo + "/" +magAmo;
    }
    void Fire(){
        Ray ray = new Ray(camera.transform.position,camera.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin,ray.direction,out hit,100f)){

            PhotonNetwork.Instantiate(hitVFX.name,hit.point,Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>()){
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All, damage);
            }
        }
    }

}
