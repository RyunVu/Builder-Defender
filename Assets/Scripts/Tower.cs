using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{

    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float shootTimerMax;

    private float shootTimer;
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 spawnPosition;

    private void Awake() {
        spawnPosition = projectileSpawnPosition.position;
    }

    private void Update() {
        HandleTargeting();
        HanldeShooting();
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }

    private void HanldeShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f ) {
            shootTimer += shootTimerMax;
            if (targetEnemy != null) {
                ArrowProjectile.Create(spawnPosition, targetEnemy);
            }
        }
        

    }

    private void LookForTarget() {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2d in collider2DArray) {
            Enemy enemy = collider2d.GetComponent<Enemy>();
            if (enemy != null) {
                // Check for current target
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                }

                // If not we already have a target
                else {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position)) {
                        // If Closer that the foreign target
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
