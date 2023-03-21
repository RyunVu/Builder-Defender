using UnityEngine;

public static class UtilsClass{

    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPositison() {
        if(mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
