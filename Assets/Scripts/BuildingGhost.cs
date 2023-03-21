using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour{

    [SerializeField] private GameObject spriteGameObject;

    private void Awake() {
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingMananger_OnActiveBuildingTypeChanged;
    }

    private void BuildingMananger_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        if(e.activeBuildingType == null ) {
            Hide();
        }
        else {
            Show(e.activeBuildingType.sprite);
        }
    }

    private void Update() {
        transform.position = UtilsClass.GetMouseWorldPositison();
    }

    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.SetActive(false);
    }

}
