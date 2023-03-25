using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourcesGenerator;

    [SerializeField] private List<ResourceAmount> startingResourceAmountList;

    // Every ResourceTypeSO with match with the certain int for the value
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private ResourceTypeListSO resourceTypeList;

    private void Awake() {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }

        foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }

        //TestLogResourceAmountDictionary();
    }

    //private void Update() {
    //if (Input.GetKeyDown(KeyCode.T)) {
    //    AddResource(resourceTypeList.list[0],2);
    //    TestLogResourceAmountDictionary();
    //}

    //if (Input.GetKeyDown(KeyCode.Y)) {
    //    AddResource(resourceTypeList.list[1], 5);
    //    TestLogResourceAmountDictionary();
    //}

    //if (Input.GetKeyDown(KeyCode.U)) {
    //    AddResource(resourceTypeList.list[2], 8);
    //    TestLogResourceAmountDictionary();
    //}
    //}

    private void TestLogResourceAmountDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.resourceName + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;
        OnResourcesGenerator?.Invoke(this, EventArgs.Empty);
        //TestLogResourceAmountDictionary();
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount)
                // Can't afford any
                return false;
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
