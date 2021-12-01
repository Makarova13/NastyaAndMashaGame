using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private readonly Creature player;

    private Vector3 movementDelta;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDelta = context.ReadValue<Vector3>();

        if (movementDelta.y > 0)
        {
            movementDelta.y = player.JumpHeight;
        }
    }

    public void FixedUpdate()
    {
        gameObject.transform.position += movementDelta * Time.deltaTime * player.Speed;
    }
}
