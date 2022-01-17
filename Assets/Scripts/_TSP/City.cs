using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    //Unity components
    [SerializeField] private Image cityImg;
    [SerializeField] private Text cityIdx;

    //Getters
    public Image GetCityImg() => cityImg;
    public string GetCityIdx() => cityIdx.text;

    //Position Coords
    public int X { get; private set; }
    public int Y { get; private set; }

    public void InitCity()
    {
        X = Random.Range(GlobalStats.MinX, GlobalStats.MaxX);
        Y = Random.Range(GlobalStats.MinY, GlobalStats.MaxY);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(X, Y);
        cityImg.color = RandColor();
        cityIdx.text = "";
    }

    public void SetCityIdx(int idx)
    {
        cityIdx.text = idx + "";
    }

    public int GetDistanceFromCity(City otherCity) => GetDistance(otherCity.X, otherCity.Y);

    public void ShowLocalPosition() => Debug.Log(this.GetComponent<RectTransform>().anchoredPosition);

    private int GetDistance(int x, int y)
    {
        int xdiff = X - x;
        int ydiff = Y - y;

        return (int)System.Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
    }

    private Color RandColor()
    {
        return new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), 1.0f);
    }
}
