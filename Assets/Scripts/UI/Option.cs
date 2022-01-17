using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private TMP_InputField valueTextOpt;
    [SerializeField] private Slider slider;

    public float ValueOpt { get; private set; }

    private void Start()
    {
        ValueOpt = slider.value;
        SetUIComponents();
    }

    public void OnSliderValueChange(float newValue)
    {
        ValueOpt = newValue;
        SetUIComponents();
    }

    public void OnInputValueChange(string newText)
    {
        ValidValue(newText, out int newValue);
    }

    private void ValidValue(string newText, out int newValue)
    {
        bool validNewValue = int.TryParse(newText, out newValue);
        if (validNewValue)
        {
            ValueOpt = newValue;
            SetUIComponents();
        }
    }

    private void SetUIComponents()
    {
        valueTextOpt.text = ValueOpt + "";
        slider.value = ValueOpt;
    }
}
