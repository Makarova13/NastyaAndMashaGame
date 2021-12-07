using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    public void OnLook(InputAction.CallbackContext context)
    {
        var rotationVector = context.ReadValue<Vector2>();

        camera.transform.Rotate(new Vector3(0, rotationVector.y, 0));
    }
}
