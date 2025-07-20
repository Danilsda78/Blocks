using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Color> colors;
    private float t1 = 0;
    private float t2 = 0;

    public void Drag()
    {
        var max = GetComponent<Slider>().maxValue;
        var value = GetComponent<Slider>().value / max * 100f;
        
        if(value > 98f)
        {
            t1 = 0;
            t2 = 0;
        }

        if (value >= 50f)
        {
            t2 = 0;
            image.color = Color.Lerp(colors[2], colors[1], t1);
            t1 += 0.005f;
        }
        else
        {
            t1 = 0;
            image.color = Color.Lerp(colors[1], colors[0], t2);
            t2 += 0.005f;
        }
    }
}
