using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject{

    public string nameBuilding;

    public Transform prefab;

    public ResourceGeneratorData resourceGeneratorData;

}
