using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour{

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barTransform;
    [SerializeField] private Transform separatorContainer;

    private void Start() {
        ContrucHealthbarSeparator();

        healthSystem.OnDamagedTaken += HealthSystem_OnDamagedTaken;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        healthSystem.OnMaxHealthChange += HealthSystem_OnMaxHealthChange;
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnMaxHealthChange(object sender, System.EventArgs e) {
        ContrucHealthbarSeparator();
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamagedTaken(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(),1,1);
    }

    private void UpdateHealthBarVisible() {
        if (healthSystem.IsFullHealth()) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }

    }

    private void ContrucHealthbarSeparator() {
        // Making health bar more specific
        Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        // Destroy old separator
        foreach (Transform separatorTransform in separatorContainer) {
            if (separatorTransform == separatorTemplate) continue;
                Destroy(separatorTransform.gameObject);
        }

        int healthAmountSeparator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();

        int healthSeparatorCount = Mathf.RoundToInt(healthSystem.GetHealthAmountMax() / healthAmountSeparator);

        for (int i = 1; i < healthSeparatorCount; i++) {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountSeparator, 0, 0);
        }
    }

}
