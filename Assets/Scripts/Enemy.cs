using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public static Enemy Create(Vector3 position) {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Rigidbody2D rigidbody2d;
    private Transform targetTransform;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void Update() {
        HandleMovement();
        HandleTargeting();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null) {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(this.gameObject);
        }
    }

    private void HandleMovement() {
        if (targetTransform.position != null) {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rigidbody2d.velocity = moveDir * moveSpeed;
        }
        else {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }

    private void LookForTarget() {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2d in collider2DArray) {
            Building building = collider2d.GetComponent<Building>();
            if (building != null) {
                // Check for current target
                if (targetTransform == null) {
                    targetTransform = building.transform;
                }

                // If not we already have a target
                else {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position)) {
                        // If Closer that the foreign target
                        targetTransform = building.transform;
                    }
                }
            }
            
            if (targetTransform == null) {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }
}
