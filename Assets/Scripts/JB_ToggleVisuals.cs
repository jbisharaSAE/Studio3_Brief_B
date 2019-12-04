using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class JB_ToggleVisuals : MonoBehaviour
{
    public Toggle toggleButton;
    public GameObject playerCam;

    private GameObject particleEffects;

    private void Start()
    {
        particleEffects = GameObject.Find("ParticleEffects");
    }

    public void SetVisualEffect()
    {
        if (toggleButton.GetComponent<Toggle>().isOn)
        {
            // visual effects on
            Debug.Log("VISUALS ON");
            SwitchPerformance(true);
        }
        else
        {
            // visual effects off
            Debug.Log("VISUALS OFF");
            SwitchPerformance(false);
        }
    }

    public void SwitchPerformance(bool onOff)
    {
        if (onOff)
        {
            particleEffects.SetActive(true);
            playerCam.GetComponent<PostProcessLayer>().enabled = true;
            QualitySettings.masterTextureLimit = 1;
        }
        else
        {
            particleEffects.SetActive(false);
            playerCam.GetComponent<PostProcessLayer>().enabled = false;
            QualitySettings.masterTextureLimit = 0;
        }
    }
}
