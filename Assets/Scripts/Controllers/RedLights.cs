using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedLights : MonoBehaviour
{
    public float min = 1f;
    public float max = 1f;
    public float time = 1f;

    //Bad
    public float intensity;

    private List<Light> lights;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        lights = new List<Light>();
        GetAllLights();
        intensity = lights[0].intensity;
        StartCoroutine("fluctuateLight");
    }

    private void GetAllLights()
    {
        Light temp = null;
        foreach (Transform childTransform in this.transform)
        {
            temp = childTransform.GetComponentInChildren<Light>();
            if (temp != null)
            {
                lights.Add(temp);
            }
            temp = null;/**/
        }
    }

    private IEnumerator fluctuateLight()
    {
        float totalTimePassed = 0;
        float temp;
        while (true)
        {
            totalTimePassed += Time.deltaTime;
            temp = (Mathf.Sin(totalTimePassed * (1/time)) + 1) / 2 * (intensity-min) + min;
            foreach (Light light in lights)
            {

                light.intensity = temp;
            }
            yield return null;
        }
    }
}

