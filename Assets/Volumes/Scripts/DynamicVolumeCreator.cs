using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class DynamicVolumeCreator : MonoBehaviour
{
    private Volume dynamicVolume;
    private VolumeProfile profile;
    private string gameobjectName = "Dynamic Volume";
    
    private void Start()
    {
        CreateDynamicVolume();
    }

    private void CreateDynamicVolume()
    {
        GameObject volumeObject = new GameObject(gameobjectName);
        dynamicVolume= volumeObject.AddComponent<Volume>();

        dynamicVolume.isGlobal= true;
        dynamicVolume.priority = 10;

        profile= ScriptableObject.CreateInstance<VolumeProfile>();
        dynamicVolume.profile = profile;


    }

    public void SetBloom(float intensity)
    {
        if(!profile.TryGet<Bloom>(out var bloom))
        {
            bloom= profile.Add<Bloom>();
        }

        bloom.intensity.Override(intensity);
        bloom.threshold.Override(1.1f);

        bloom.active = true;
    }

    public void SetColorContrast(float constantValue)
    {
        if(!profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj= profile.Add<ColorAdjustments>();
        }

        colorAdj.contrast.Override(constantValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
         SetBloom(5f);
         SetColorContrast(20f);
        }
    }
}
