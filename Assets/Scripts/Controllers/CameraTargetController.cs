using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int sensitivity = 5;

    private Vector3 lookDirection;
    private Vector3 turnDirection;
    private PlayerController playerController;

    public void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        playerController.Turned += OnPlayerTurned;
        //GetComponent<Renderer>().enabled = false;
        //gameObject.transform.position = player.transform.position + Vector3.forward;
    }

    public void FixedUpdate()
    {
        gameObject.transform.position += lookDirection.normalized.x * transform.right * Time.deltaTime * sensitivity;
        gameObject.transform.position += lookDirection.normalized.y * transform.up * Time.deltaTime * sensitivity;
        gameObject.transform.position += playerController.MovementDelta;
        gameObject.transform.position += turnDirection;

        transform.rotation = player.transform.rotation;
    }

    public void OnLook(InputAction.CallbackContext context)
    { 
        lookDirection = context.ReadValue<Vector2>();
    }

    private void MoveCameraTarget(float delta)
    {
        //if(delta > )
    }

    private void OnPlayerTurned(float position)
    {
        turnDirection = position * transform.right;
    }
}