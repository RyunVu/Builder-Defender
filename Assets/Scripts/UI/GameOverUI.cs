using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour{

    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI wavesSurvivedText;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button mainMenuBtn;


    private void Awake() {
        Instance = this;

        retryBtn.onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        mainMenuBtn.onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);

        wavesSurvivedText.text = "YOU SURVIVIED " + EnemyWaveManager.Instance.GetWaveNumber() + " WAVES!";
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
