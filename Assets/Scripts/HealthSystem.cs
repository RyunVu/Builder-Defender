using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour{

    public event EventHandler OnDamagedTaken;
    public event EventHandler OnDied;


    [SerializeField] private int healthMax;
    private int healthAmount;

    private void Awake() {
        healthAmount = healthMax;
    }

    public void Damage(int damgeAmount) {
        healthAmount -= damgeAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthMax);

        OnDamagedTaken?.Invoke(this, EventArgs.Empty);

        if (IsDead()) {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead() {
        return healthAmount == 0;
        
    }

    public bool IsFullHealth() {
        return healthAmount == healthMax;
    }

    public int GetHealthAmount(){
        return healthAmount;
    }

    public float GetHealthAmountNormalized() {
        return (float)healthAmount / healthMax; 
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount) {
        this.healthMax = healthAmountMax;

        if(updateHealthAmount) {
            healthAmount = healthAmountMax;
        }
    }

}
