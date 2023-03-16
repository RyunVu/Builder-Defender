using UnityEngine;

public class BuildingManager : MonoBehaviour{

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO buildingType;

    private void Start() {
        mainCamera = Camera.main;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(buildingType.prefab, GetMouseWorldPositison(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            buildingType = buildingTypeList.list[0];
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            buildingType = buildingTypeList.list[1];
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            buildingType = buildingTypeList.list[2];
        }
    }

    private Vector3 GetMouseWorldPositison() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

}
