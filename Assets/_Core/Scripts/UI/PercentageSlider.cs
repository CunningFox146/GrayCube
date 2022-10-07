using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class PercentageSlider : MonoBehaviour
    {
        public event Action<float> PercentageChanged;

        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _percentageText;

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        public void SetPercent(float percent)
        {
            _slider.value = percent;
        }

        private void RegisterEventHandlers()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChangedHandler);
        }

        private void UnregisterEventHandlers()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChangedHandler);
        }

        private void OnSliderValueChangedHandler(float percentage)
        {
            _percentageText.text = $"{Mathf.FloorToInt(percentage * 100f)}%";
            PercentageChanged?.Invoke(percentage);
        }
    }
}