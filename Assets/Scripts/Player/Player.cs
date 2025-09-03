using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] float horizontalSpeed = 10;

    PhotonView photonView;

    Rigidbody rigidBody;

    [SerializeField] int vida = 100;

    [SerializeField] UnityEngine.UI.Image hpBar;

    [SerializeField] int enterDamage;

    #endregion


    #region Unity MonoBehaviour Methods

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        rigidBody.linearVelocity = new Vector3(horizontalAxis * horizontalSpeed, rigidBody.linearVelocity.y, verticalAxis * horizontalSpeed);

        if (Input.GetKeyDown(KeyCode.Escape)) { photonView.RPC("TakeDamage", RpcTarget.All); }
    }

    #endregion


    #region Custom Methods


    [PunRPC]
    public void TakeDamage()
    {
        if(enterDamage != 0)
        {


            vida -= enterDamage;

        }
        else { vida -= 10; }

            hpBar.fillAmount = vida * 0.01f;
        enterDamage = 0;

    }




    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            enterDamage = other.gameObject.GetComponent<BulletScript>().damage;

            photonView.RPC("TakeDamage",RpcTarget.All);
           // other.gameObject.GetComponent<BulletScript>().DestroyBullet();
        
        }
    }


}
