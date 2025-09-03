using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] Rigidbody rb;
    void Start()
    {
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

    public void DestroyBullet()
    {
        Destroy(this.gameObject, 3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(this);
        }
    }
}
