using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public static Enemy Create(Vector3 position) {
        //Transform pfEnemy = Resources.Load<Transform>(GameAssets.Instance.pfEnemy);
        Transform enemyTransform = Instantiate(GameAssets.Instance.pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Rigidbody2D rigidbody2d;
    private Transform targetTransform;
    private HealthSystem healthSystem;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHQBuilding() != null) {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamagedTaken += HealthSystem_OnDamagedTaken;
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDamagedTaken(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
        CineMachineShake.Instance.ShakeCamera(3f, .1f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        CineMachineShake.Instance.ShakeCamera(5f, .15f);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
            this.healthSystem.Damage(999);
        }
    }

    private void HandleMovement() {
        if (targetTransform != null) {
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

                if (BuildingManager.Instance.GetHQBuilding() != null) {
                    targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
                }
            }
        }
    }
}
