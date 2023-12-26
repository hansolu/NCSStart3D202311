using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    MeshRenderer renderer;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    public void SetChangeColor(Color color)
    {
        renderer.material.color = color;
    }
}
