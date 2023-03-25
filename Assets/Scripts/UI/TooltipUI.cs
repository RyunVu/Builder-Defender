using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour{

    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private RectTransform background;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;

    private void Awake() {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update() {
        HandleFollowMouse();
        if (tooltipTimer != null) {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0)
                Hide();
        }
    }

    private void HandleFollowMouse() {

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        // Set tooltip in the screen view if mouse move out of srceen
        if (anchoredPosition.x + background.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - background.rect.width;
        }
        if (anchoredPosition.y + background.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - background.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;

    }

    private void SetText(string tooltipText) {
        textMeshPro.text = tooltipText;
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new(16, 8);
        background.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null) {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public class TooltipTimer {
        public float timer;
    }
}
