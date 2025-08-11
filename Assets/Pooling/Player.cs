using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    private Rigidbody rb;

    [Header("Shoot")]
    [SerializeField] Transform firePoint;
    [SerializeField] Pool_Manager poolManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, z * speed);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var obj = poolManager.GetPoolObject();

            if (obj != null)
            {   
                obj.SetActive(true);
                obj.GetComponent<Bullet>().StartDesactivation();
                obj.transform.position = firePoint.position;
            }
        }
    }
}