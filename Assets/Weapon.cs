using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Linq;

using Photon.Pun.UtilityScripts;

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

    [Header("Recoil Settings")]
    // [Range(0,1)]
    // public float recoilPercent = 0.3f;
    [Range(0,2)]
    public float recoverPercent = 0.7f;
    [Space]
    public float recoilUp = 1f;

    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    private bool recovering;
    void start(){

        magText.text = mag.ToString();
        amoText.text = amo + "/" +magAmo;

        originalPosition = transform.position;

        recoilLength = 0;
        recoverLength = 1/ firerate*recoverPercent;
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
        if(Input.GetKeyDown(KeyCode.R) && mag > 0){
            Reload();
        }

        if(recoiling){
            Recoil();
        }
        if(recovering){
            Recovering();
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

        recoiling =  true;
        recovering = false;
        Ray ray = new Ray(camera.transform.position,camera.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin,ray.direction,out hit,100f)){

            PhotonNetwork.Instantiate(hitVFX.name,hit.point,Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>()){

                PhotonNetwork.LocalPlayer.AddScore(damage);
                if(damage > hit.transform.gameObject.GetComponent<Health>().health){
                    PhotonNetwork.LocalPlayer.AddScore(100);
                }
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All, damage);
            }
        }
    }

    void Recoil(){
        Vector3 finalPosition = new Vector3(originalPosition.x,originalPosition.y,originalPosition.z-recoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,finalPosition,ref recoilVelocity,recoilLength);

        if(transform.localPosition == finalPosition){
            recoiling = false;
            recovering = true;

        }
    }

    void Recovering(){
        Vector3 finalPosition = originalPosition;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,finalPosition,ref recoilVelocity,recoverLength);

        if(transform.localPosition == finalPosition){
            recoiling = false;
            recovering = false;
            
        }
    }

}
