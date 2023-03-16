using UnityEngine;

public class BuildingManager : MonoBehaviour{

    [SerializeField] private Transform pfWoodHarvester;


    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(pfWoodHarvester, GetMouseWorldPositison(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPositison() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

}
