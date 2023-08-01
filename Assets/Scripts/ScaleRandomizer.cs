using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    [Header("Non-Uniform Scaling")]
    [SerializeField] private Vector3 scaleMinimum;
    [SerializeField] private Vector3 scaleMaximum;

    [Header("Uniform Scaling")]
    [SerializeField] private bool enableUniformScaling;
    [SerializeField] private Vector2 uniformScaleMinMax;

    private void Awake()
    {
        if (enableUniformScaling)
        {
            float f = Random.Range(uniformScaleMinMax.x, uniformScaleMinMax.y);

            transform.localScale = new Vector3(f, f, f);
        }

        else
        {
            float x = Random.Range(scaleMaximum.x, scaleMaximum.x);
            float y = Random.Range(scaleMaximum.y, scaleMaximum.y);
            float z = Random.Range(scaleMaximum.z, scaleMaximum.z);

            transform.localScale = new Vector3(x, y, z);
        }
    }
}
