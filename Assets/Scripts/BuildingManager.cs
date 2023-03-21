using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour{

    public static BuildingManager Instance { get; private set; }

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private void Awake() {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start() {
        mainCamera = Camera.main;

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null) {
                Instantiate(activeBuildingType.prefab, GetMouseWorldPositison(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetMouseWorldPositison() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

}
