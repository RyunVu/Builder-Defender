using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour{

    public event EventHandler OnWaveNumberChanged;
    private enum State {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextWaveSpawnPostion;

    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;

    private void Start() {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPostion.position = spawnPosition;
        nextEnemySpawnTimer = 3f;
    }

    private void Update() {
        HandleWaveSpawn();
    }

    private void SpawnWave() {
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;

        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleWaveSpawn() {
        switch (state) {

            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer <= 0) {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0) {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f) {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if(remainingEnemySpawnAmount <= 0) {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPostion.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;

            default:
                break;
        }
    }

    public int GetWaveNumber() {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer() {
        return nextWaveSpawnTimer;
    }
    public Vector3 GetSpawnPosition() {
        return spawnPosition;
    }

}
