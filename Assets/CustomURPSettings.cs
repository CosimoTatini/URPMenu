using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomURPSettings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Volume globalVolume;

    private Resolution[] resolutions;


    private void Start()
    {
        InitializeResolution();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    private void InitializeResolution()
    {



        Resolution[] availableResolutions = Screen.resolutions;

        List<Resolution> uniqueResolutions = new List<Resolution>();


        for (int i = 0; i < availableResolutions.Length; i++)
        {
            bool found = false;
            foreach (Resolution res in uniqueResolutions)
            {

                if (res.width == availableResolutions[i].width && res.height == availableResolutions[i].height)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                uniqueResolutions.Add(availableResolutions[i]);
            }
        }


        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);


            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    [System.Obsolete]
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        FullScreenMode fullScreen = Screen.fullScreenMode;
        Screen.SetResolution(resolution.width, resolution.height, fullScreen, resolution.refreshRateRatio);
        Debug.Log("Ho cambiato risoluzione");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Schermo intero" + (isFullscreen ? "enabled" : "disabled"));
    }

    public void TogglePostProcessing(bool isEnabled)
    {
        if (globalVolume != null)
        {
            globalVolume.enabled = isEnabled;
        }
        Debug.Log("Post Processing " + (isEnabled ? "enabled" : "disabled"));
    }
}
