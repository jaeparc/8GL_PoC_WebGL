using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("--- REFERENCES ---")]
    public Toggle Fullscreen;
    public TMP_Dropdown Resolutions;
    public TMP_Dropdown Quality;

    private Resolution[] _res;
    // Start is called before the first frame update
    void Start()
    {
        _res = Screen.resolutions;
        Resolutions.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < _res.Length; i++)
        {
            options.Add(_res[i].width + " x " + _res[i].height);
        }

        Resolutions.AddOptions(options);
        Resolutions.RefreshShownValue();
        
        Quality.ClearOptions();
        List<string> qualityOptions = new List<string>(QualitySettings.names);
        Quality.AddOptions(qualityOptions);
        Quality.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFullscreen()
    {
        Screen.fullScreen = Fullscreen.isOn;
    }

    public void SetResolution()
    {
        int index = Resolutions.value;
        Resolution resolution = _res[index];
        Screen.SetResolution(resolution.width, resolution.height, Fullscreen.isOn);
    }

    public void SetQuality()
    {
        int index = Quality.value;
        QualitySettings.SetQualityLevel(index);
    }
}
