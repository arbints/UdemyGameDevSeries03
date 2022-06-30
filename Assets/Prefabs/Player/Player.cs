using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController; 
    [SerializeField] float moveSpeed = 20f; 
    Vector2 moveInput;

    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += moveStickmoveStick;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void moveStickmoveStick(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        characterController.Move((rightDir * moveInput.x + upDir * moveInput.y) * Time.deltaTime * moveSpeed);
        if(moveInput.magnitude != 0 && cameraController!=null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }
}
