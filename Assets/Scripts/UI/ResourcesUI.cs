using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour {

    [SerializeField] private Transform resourceTemplate;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceAmountText;

    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;


    private void Awake() {

        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        resourceTemplate.gameObject.SetActive(false);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.resourceSprite;
            resourceImage.sprite = resourceTransform.Find("image").GetComponent<Image>().sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
        }
    }

    private void Start() {
        UpdateResourceAmount();
        ResourceManager.Instance.OnResourcesGenerator += ResourceManager_OnResourcesGenerator;

    }

    private void ResourceManager_OnResourcesGenerator(object sender, System.EventArgs e) {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceAmountText = resourceTransform.Find("text").GetComponent<TextMeshProUGUI>();
            resourceAmountText.text = resourceAmount.ToString();
        }
    }

}
