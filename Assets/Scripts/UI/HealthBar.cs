using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour{

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform barTransform;

    private void Start() {
        healthSystem.OnDamagedTaken += HealthSystem_OnDamagedTaken;
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

}
