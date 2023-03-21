using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float orthographicSize;
    private float targetOrthographicSize;

    private Vector2 lastMousePosition;
    private bool dragPanMoveActive;


    private void Start() {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update() {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement() {
        Vector3 inputDir = new Vector3(0, 0, 0);
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(1)) {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)) { 
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive) {
            Vector2 mouseMovementDelta = -((Vector2)Input.mousePosition - lastMousePosition);

            float movePanSpeed = 10f;

            inputDir.x = mouseMovementDelta.x * movePanSpeed;
            inputDir.y = mouseMovementDelta.y * movePanSpeed;

            lastMousePosition = Input.mousePosition;
        }



        Vector3 moveDir = new Vector3(inputDir.x, inputDir.y).normalized;

        float moveSpeed = 2.5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime * orthographicSize;
    }

    private void HandleZoom() {

        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

}
