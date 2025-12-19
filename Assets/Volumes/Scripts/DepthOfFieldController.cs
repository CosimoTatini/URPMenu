using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthOfFieldController : MonoBehaviour
{
    public Volume volume;
    private DepthOfField depthOfField;
    private GameObject targetObject;

    private void Start()
    {
        if(volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.focusDistance.overrideState = true;
            depthOfField.active = true;
        }
    }

    private void Update()
    {
        if (depthOfField == null) return;

        float targetDistance= Vector3.Distance(transform.position,targetObject.transform.position);

        depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, targetDistance, Time.deltaTime);
    }
}
