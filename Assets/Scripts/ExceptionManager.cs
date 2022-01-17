using UnityEngine;
public class ExceptionManager : MonoBehaviour
{
    [SerializeField] private MainPanelManager mainPanelManager;

    void Awake() => Application.logMessageReceived += HandleException;

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            mainPanelManager.SetFeedbackText(GlobalStats.feedbackTexts[3]);
            mainPanelManager.SetStartButtonInteractable(true);
            GlobalStats.OutputList.Add(logString);
            GlobalStats.OutputList.Add(System.Environment.NewLine);
        }
    }
}