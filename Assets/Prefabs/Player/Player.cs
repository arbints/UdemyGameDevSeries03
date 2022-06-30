using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController; 
    [SerializeField] float moveSpeed = 20f; 
    [SerializeField] float turnSpeed = 30f; 
    
    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += moveStickUpdated;
        aimStick.onStickValueUpdated += aimStickUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void aimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
    }

    void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 MoveDir = StickInputToWorldDir(moveInput);
        characterController.Move(MoveDir * Time.deltaTime * moveSpeed);

        UpdateAim(MoveDir);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

    private void UpdateCamera()
    {
        //player is move but not aiming, and cameraController exists
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {
        if (AimDir.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);
        }
    }
}
