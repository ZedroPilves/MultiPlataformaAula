using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimTopDown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInputs;
    [SerializeField] private Camera mainCamera; // Referência à câmera top-down na cena

    [Header("Settings")]
    [SerializeField] private LayerMask floorMask; // Layer do chão para raycast
    [SerializeField] Transform ShootPos;
    [SerializeField] GameObject bullet;

    private InputAction lookAction;
      

    [SerializeField] PhotonView photonView;

    void Start()
    {

       photonView = GetComponent<PhotonView>(); 
        playerInputs = GetComponent<PlayerInput>();
        lookAction = playerInputs.actions["Look"];

        if (mainCamera == null)
            mainCamera = Camera.main;

        // No need to lock cursor in top-down
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Shoot()
    {

       Instantiate(bullet, ShootPos.position, this.gameObject.transform.rotation);  
    }
    void Update()
    {
        // Pega a posição do mouse na tela
        

        if (photonView.IsMine) {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Cria um raio da câmera até o ponto do mouse na tela
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, floorMask))
            {
                Vector3 targetPoint = hit.point;

                // Zera altura do target para o player não inclinar
                targetPoint.y = transform.position.y;

                // Direção do jogador para o ponto do mouse no chão
                Vector3 direction = (targetPoint - transform.position).normalized;

                if (direction.sqrMagnitude > 0.01f)
                {
                    // Rotaciona o jogador para olhar o ponto
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = lookRotation;
                }
            }
            if (Input.GetButtonDown("Fire1")){
            Shoot();        
        }






        }
    }
}
