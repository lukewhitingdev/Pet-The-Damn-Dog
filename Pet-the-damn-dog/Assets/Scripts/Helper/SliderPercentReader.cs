using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPercentReader : MonoBehaviour
{
    // For use as a component
    [SerializeField] private Slider compSlider;
    private TextMeshProUGUI sliderPercentText;

    public void Awake()
    {
        try { sliderPercentText = GetComponent<TextMeshProUGUI>(); }
        catch (System.Exception)
        {
            Debug.LogError("Tried to use slider percent reader as a component on non-text object!");
            throw;
        }
    }

    public void updateSliderPercent()
    {
        sliderPercentText.text = getSliderPercent(compSlider).ToString("0") + "%";
    }

    // For use as a helper class
    public static float getSliderPercent(Slider slider)
    {
        return slider.value * (100 / slider.maxValue);
    }
}
