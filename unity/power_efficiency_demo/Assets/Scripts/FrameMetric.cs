using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FrameMetric : MonoBehaviour
{
        
    [SerializeField] private Text frameRate;

    private void Awake()
    {
        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        while (enabled)
        {
            var deltaTime = Time.deltaTime;
            var fps = (int)(1f / deltaTime);
            frameRate.text = $"FPS: {fps}";
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}