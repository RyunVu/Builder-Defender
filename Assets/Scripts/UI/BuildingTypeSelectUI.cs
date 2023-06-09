using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Transform btnTemplate;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private Transform arrowBtn;

    private void Awake() {
        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        
        btnTemplate.gameObject.SetActive(false);
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        // For the arrow btn
        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(-10, -30);

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvent mouseEnterExitEvent = arrowBtn.GetComponent<MouseEnterExitEvent>();
        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) => {
            TooltipUI.Instance.Show("Cursor");
        };
        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => {
            TooltipUI.Instance.Hide();
        };


        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) => {
                TooltipUI.Instance.Show(buildingType.nameBuilding + "\n" + buildingType.GetContructionResourceCostString());
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };
            btnTransformDictionary[buildingType] = btnTransform;
        }
    }


    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        arrowBtn.Find("selected").gameObject.SetActive(false);

        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys) {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if(activeBuildingType == null) {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else {
            btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }

}
