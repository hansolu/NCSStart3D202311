using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndChangeColor : MonoBehaviour
{
    public Camera cam;
    Color[] colors = new Color[5] { Color.red, Color.green, Color.blue, Color.black, Color.white};
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))//ī�޶�κ��� �������� (���콺 ��ġ����)
            {                
                if (hit.collider.CompareTag("Enemy"))
                {
                    ColorChange color = hit.collider.GetComponent<ColorChange>();
                    if (color != null)
                        color.SetChangeColor(colors[ Random.Range(0, colors.Length)]);
                }                                
            }

            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())  //UI�� Ŭ���ߴ��� ���θ� �� �� ����.
            {
                Debug.Log("UIŬ����");   
            }                    
        }    
    }
}
