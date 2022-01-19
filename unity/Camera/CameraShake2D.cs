using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Add more juicing classes like smooth following.
// TODO: Maybe add some more options for slomo, etc.

// juice up the camera with some camera shake
public class CameraShake2D: MonoBehaviour
{
    private bool dirty = false;
    private float friction = 1f;
    private float stress = 0f;
    private float shake { get { return stress * stress; } }
    private float maxAngle = 10f;
    private float maxOffset = 0.5f;
    private Transform initialTransform = null;

    

    void Update()
    {
        if(stress != 0)
        {
            if(!dirty)
            {
                initialTransform = transform;
                dirty = true;
            }
            ApplyStress();
            ApplyFriction();
        }
    }
    
    public void Add(float value)
    {
        stress = Mathf.Clamp01(stress + value);
    }

    private void ApplyStress()
    {
        var seed = Random.Range(-100f, 100f);
        float offsetX = maxOffset * shake * GetNoise(seed); // x offset
        float offsetY = maxOffset * shake * GetNoise(seed + 1); // y offset
        float angle = maxAngle * shake * GetNoise(seed + 2); // angle
        transform.position = initialTransform.position + new Vector3(offsetX, offsetY, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialTransform.rotation.z + angle));
    }

    private void ApplyFriction()
    {
        stress -= friction * Time.deltaTime; // remove stress over time
        if (stress <= 0)
        {
            stress = 0;
            dirty = false;
        }
    }

    private float GetNoise(float seed)
    {
        return Mathf.PerlinNoise(seed, seed) * Time.timeScale * Random.Range(-1, 1);
    }
}

