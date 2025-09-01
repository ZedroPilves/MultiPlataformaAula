using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] float horizontalSpeed = 10;

    PhotonView photonView;

    Rigidbody rigidBody;

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



    #endregion



}
