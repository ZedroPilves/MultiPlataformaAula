using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Microsoft.Unity.VisualStudio.Editor;
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
    }

    #endregion


    #region Custom Methods

    public void TakeDamage(int dmg)
    {
        vida-=dmg;
        hpBar.fillAmount = vida * 0.1f;

    }

    #endregion



}
