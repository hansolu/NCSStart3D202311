using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    GraphicRaycaster graphicRay;
    public GraphicRaycaster GraphicRay=> graphicRay;

    void Start()
    {
        graphicRay = GetComponent<GraphicRaycaster>();
    }
}
