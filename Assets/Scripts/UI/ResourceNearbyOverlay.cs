using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake() {
        Hide();
    }


    private void Update() {
        float nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float precent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        //if (nearbyResourceAmount == 0) {
        //    precent = 0;
        //}
        transform.Find("text").GetComponent<TextMeshPro>().SetText(precent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData) {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.resourceSprite;

    }

    public void Hide() {
        gameObject.SetActive(false);
    }

}
