using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Every RTso with match with the certain int for the value
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private ResourceTypeListSO resourceTypeList;

    private void Awake() {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary();

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            AddResource(resourceTypeList.list[0],2);
            TestLogResourceAmountDictionary();
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            AddResource(resourceTypeList.list[1], 5);
            TestLogResourceAmountDictionary();
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            AddResource(resourceTypeList.list[2], 8);
            TestLogResourceAmountDictionary();
        }
    }

    private void TestLogResourceAmountDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.resourceName + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;
    }

}
