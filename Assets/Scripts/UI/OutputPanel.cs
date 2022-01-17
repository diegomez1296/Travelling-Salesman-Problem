using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputPanel : MonoBehaviour
{
    [SerializeField] private Transform outputContent;
    [SerializeField] private TextMeshProUGUI outputTextPrefab;
    [Space]
    [SerializeField] private Image outputButton;
    [SerializeField] private Sprite activeOutput;
    [SerializeField] private Sprite deactiveOutput;

    private List<TextMeshProUGUI> outputTextsList = new List<TextMeshProUGUI>();
    public void ChangeVisibility()
    {
        if(!this.gameObject.activeSelf)
        {
            CreateOutputList();
            outputButton.sprite = activeOutput;
            this.gameObject.SetActive(true);
        }
        else
        {
            ClearOutputList();
            outputButton.sprite = deactiveOutput;
            this.gameObject.SetActive(false);
        }
    }

    private void CreateOutputList()
    {
        foreach (var item in GlobalStats.OutputList)
        {
            var outputLine = Instantiate(outputTextPrefab, outputContent);
            outputLine.text = item;
            outputTextsList.Add(outputLine);
        }
        AddAuthorData();
    }

    private void ClearOutputList()
    {
        foreach (var item in outputTextsList)
        {
            Destroy(item.gameObject);
        }
        outputTextsList.Clear();
    }

    private void AddAuthorData()
    {
        var authorData = Instantiate(outputTextPrefab, outputContent);
        authorData.text = System.Environment.NewLine +
                            "Problem Komiwoja¿era" + System.Environment.NewLine +
                            "Autor: £ukasz Rydziñski" + System.Environment.NewLine +
                            "Informatyka Gier Komputerowych";
        outputTextsList.Add(authorData);
    }
}
