using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour{

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHeal = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHeal / 2;

            ResourceAmount[] resourcesCost = new ResourceAmount[] {
                new ResourceAmount {
                    resourceType = goldResourceType,
                    amount = repairCost
                }
            };

            if (ResourceManager.Instance.CanAfford(resourcesCost)) {
                // Can afford the repairs
                ResourceManager.Instance.SpendResources(resourcesCost);
                healthSystem.HealFull();
            }
            else {
                TooltipUI.Instance.Show("Can't afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }

}
