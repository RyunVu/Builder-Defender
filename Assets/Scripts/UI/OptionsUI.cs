using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{

    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private Button _soundIncreaseBtn;
    [SerializeField] private Button _soundDecreaseBtn;
    [SerializeField] private Button _musicIncreaseBtn;
    [SerializeField] private Button _musicDecreaseBtn;
    [SerializeField] private TextMeshProUGUI _soundVolumeText;
    [SerializeField] private TextMeshProUGUI _musicVolumeText;

    private void Awake() {

        _soundIncreaseBtn.onClick.AddListener(() => {
            _soundManager.IncreaseVolume();
            UpdateVolumeText();
        });

        _soundDecreaseBtn.onClick.AddListener(() => {
            _soundManager.DecreaseVolume();
            UpdateVolumeText();
        });

        _musicIncreaseBtn.onClick.AddListener(() => {
            _musicManager.IncreaseVolume();
            UpdateVolumeText();
        });

        _musicDecreaseBtn.onClick.AddListener(() => {
            _musicManager.DecreaseVolume();
            UpdateVolumeText();
        });

        _mainMenuBtn.onClick.AddListener(() => {

        });

    }

    private void Start() {
        UpdateVolumeText();
        gameObject.SetActive(false);
    }

    private void UpdateVolumeText() {
        _soundVolumeText.text = Mathf.RoundToInt(_soundManager.GetVolume() * 10f).ToString();
        _musicVolumeText.text = Mathf.RoundToInt(_musicManager.GetVolume() * 10f).ToString();   
    }

    public void ToggleVisible() {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }


}
