using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public void drpQuality_changeValue(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void toogleFullscreen_chnageValue(bool value)
    {
        Screen.fullScreen = value;
    }

    public void btnBack_click()
    {
        gameObject.SetActive(false);
    }
}
