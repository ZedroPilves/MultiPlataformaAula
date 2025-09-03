using Photon.Pun;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed;
    [SerializeField] public int damage;
    [SerializeField] Rigidbody rb;
    [SerializeField] PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();    
        rb = GetComponent<Rigidbody>();
        DestroyBullet();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    public void MoveBullet()
    {
        rb.linearVelocity = transform.forward *  speed;   
        
    }

    public void DestroyBulletRPC()
    {
        photonView.RPC("DestroyBullet", RpcTarget.All);
    }

    [PunRPC]
    public void DestroyBullet()
    {
       Destroy(this.gameObject);    
    }

    
}
