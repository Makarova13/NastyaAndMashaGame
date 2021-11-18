using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float jumpHeight = 3;

    private Vector3 movementDelta;
    private Vector3 crawlDelta = new Vector3(0, 0.3f, 0);
    private bool isCrawling = false;

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("boom");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDelta = context.ReadValue<Vector3>();

        if (movementDelta.y > 0)
        {
            movementDelta.y = jumpHeight;
        }
        else if (movementDelta.y < 0 && !isCrawling)
        {
            gameObject.transform.localScale -= crawlDelta;
            isCrawling = true;
        }
        else if (isCrawling)
        {
            gameObject.transform.localScale += crawlDelta;
            isCrawling = false;
        }
    }

    public void FixedUpdate()
    {
        gameObject.transform.position += movementDelta * Time.deltaTime * speed;
    }
}
