using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject{

    public string nameBuilding;

    public Transform prefab;

    public ResourceGeneratorData resourceGeneratorData;

    public Sprite sprite;

    public float minContructionRadius;

    public ResourceAmount[] constructionResourceCostArray;

    public int healthAmountMax;

    public string GetContructionResourceCostString() {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray) {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
                resourceAmount.resourceType.nameShort + ": " + resourceAmount.amount +
                "</color> ";
        }
        return str;
    }

}
