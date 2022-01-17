using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityViewer : MonoBehaviour
{
    public static CityViewer Instance;

    [SerializeField] private GameObject root;
    [SerializeField] private Image cityIcon;
    [SerializeField] private Text cityIdx;
    [SerializeField] private Text posX;
    [SerializeField] private Text posY;

    private void Awake() => Instance = this;

    public void SetCityViever(City city)
    {
        cityIcon.sprite = city.GetCityImg().sprite;
        cityIcon.color = city.GetCityImg().color;
        cityIdx.text = city.GetCityIdx();
        posX.text = "X: " + city.X;
        posY.text = "Y: " + city.Y + "";
        root.SetActive(true);
    }

    public void HideCityViewer()
    {
        root.SetActive(false);
    }
}
