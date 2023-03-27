using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour{

    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    private void Awake() {
        _playBtn.onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        _quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
