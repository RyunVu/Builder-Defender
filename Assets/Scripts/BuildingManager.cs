using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour{

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

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
                if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPositison(), out string errorMessage)) {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray)) {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPositison(), Quaternion.identity);
                    }
                    else {
                        TooltipUI.Instance.Show("Cann't afford " + activeBuildingType.GetContructionResourceCostString(), new TooltipUI.TooltipTimer { timer = 2f });
                    }
                }
                else {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
    }


    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

    public bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear) {
            errorMessage = "Area is not clear";
            return false;
        }
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minContructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                if (buildingTypeHolder.buildingType == buildingType) {
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
        }

        float maxConstructionRadius = 30f;

        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray) {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "Too far from other building!";
        return false;
    }

}
