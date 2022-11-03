using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class reloading : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    [SerializeField] private GunData gunData;
    // Update is called once per frame
    public void currentAmmo()
    {
        text.text = gunData.currentAmmo.ToString();
    }
}
