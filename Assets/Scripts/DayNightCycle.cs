using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour{

    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _secondsPerDay = 10f;
    private Light2D _light2d;
    private float _dayTime;
    private float _dayTimeSpeed;

    private void Awake() {
        _light2d = GetComponent<Light2D>();
        _dayTimeSpeed = 1 / _secondsPerDay;
    }

    private void Update() {
        _dayTime += Time.deltaTime * _dayTimeSpeed;
        _light2d.color = _gradient.Evaluate(_dayTime % 1f);
    }
}
