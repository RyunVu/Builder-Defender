using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour{

    [SerializeField] private BuildingContruction buildingContruction;

    private Image contructionProgressImage;

    private void Awake() {
        contructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update() {
        contructionProgressImage.fillAmount = buildingContruction.GetContructionTimerNormalized();
    }

}
