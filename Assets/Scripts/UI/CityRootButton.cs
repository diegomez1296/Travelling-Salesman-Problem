using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CityRootButton : Button, IPointerEnterHandler, IPointerExitHandler
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        CityViewer.Instance.SetCityViever(transform.parent.GetComponent<City>());
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        CityViewer.Instance.HideCityViewer();
    }
}
