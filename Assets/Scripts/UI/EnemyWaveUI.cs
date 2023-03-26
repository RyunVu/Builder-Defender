using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour{

    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveMessageText;
    [SerializeField] private RectTransform enemyWaveSpawnPositionIndicator;
    [SerializeField] private RectTransform enemyClosestPositionIndicator;

    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e) {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update() {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnIndicator();
        HandleEnemyClosetPostionIndicator();
    }

    private void SetMessageText(string message) {
        waveMessageText.text = message;
    }

    private void SetWaveNumberText(string waveNumber) {
        waveNumberText.text = waveNumber;
    }

    private void HandleNextWaveMessage() {

        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f) {
            SetMessageText("");
        }
        else {
            SetMessageText("Next wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyWaveSpawnIndicator() {
        // Red - Spawn postion
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);

       
    }

    private void HandleEnemyClosetPostionIndicator() {
        // Yellow - Closet enemy
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
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

        if (targetEnemy != null) {
            Vector3 dirToClosetEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
            enemyClosestPositionIndicator.anchoredPosition = dirToClosetEnemy * 250f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosetEnemy));

            float distanceToClosetEnemy = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosetEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else {
            // No enemies alive;
            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

}
