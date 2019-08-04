using UnityEngine;  
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;  
using UnityEngine.UI;
 

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Color normalColor, highlightedColor;
    public TextMeshProUGUI theText;
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(highlightedColor==null)
        theText.color = Color.white; //Or however you do your color

        else
        {
            theText.color = highlightedColor; //Or however you do your color
        }
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        if(normalColor==null)
        theText.color = Color.black; //Or however you do your color
        else
        {
            theText.color = normalColor; //Or however you do your color
        }
    }
}