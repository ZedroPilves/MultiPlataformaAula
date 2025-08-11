using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float firePower;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = Vector3.forward * firePower;
    }

    public void StartDesactivation()
    {
        // "Invoke" � uma esp�cie de Corrotina, que consegue chamar uma fun��o com um delay se necess�rio
        Invoke(nameof(DesactivateBullet), 1.5f);
    }

    public void DesactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}