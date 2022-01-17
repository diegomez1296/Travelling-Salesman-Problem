using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelManager : MonoBehaviour
{
    [SerializeField] private Option cityAmount;
    [SerializeField] private Option population;
    [SerializeField] private Option mutationRate;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI feedbackText;

    public void SetGlobalStats()
    {
        GlobalStats.OutputList.Clear();
        GlobalStats.Cities = (int)cityAmount.ValueOpt;
        GlobalStats.Population = (int)population.ValueOpt;
        GlobalStats.MutationPercent = mutationRate.ValueOpt / 100;
    }

    public void SetFeedbackText(string newText) => feedbackText.text = newText;

    public void SetStartButtonInteractable(bool value) => startButton.interactable = value;
}
