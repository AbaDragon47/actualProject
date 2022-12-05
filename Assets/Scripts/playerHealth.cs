using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerHealth : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    // Start is called before the first frame update
    public Image fill;
    //public Target target;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color=gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        Debug.Log("setHealth method");
    }


}
