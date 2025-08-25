using UnityEngine;

public class PlayerMovinPool : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    private Rigidbody rb;

    [Header("Shoot")]
    [SerializeField] Transform firePoint;
    [SerializeField] PlayerPoolingRev pool;

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
            var obj = pool.GetBulletPool();

            if (obj != null)
            {
                obj.SetActive(true);
                obj.GetComponent<BallaScriptRev>().CallEndUse();
                obj.transform.position = firePoint.position;
            }
        }
    }
}
