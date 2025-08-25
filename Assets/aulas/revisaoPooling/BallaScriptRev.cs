using Photon.Pun.UtilityScripts;
using UnityEngine;

public class BallaScriptRev : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rb = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Move()
    {
        rb.linearVelocity = Vector3.forward * speed;
    }
    public void EndUse()
    {
        this.gameObject.SetActive(false);
    }

    public void CallEndUse()
    {
        Invoke(nameof(EndUse), 1.5f);
    }
}
