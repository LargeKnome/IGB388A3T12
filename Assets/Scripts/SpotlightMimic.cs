using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpotlightMimic : MonoBehaviour
{
    private FieldOfView FOV; // Reference to the GameObject containing the mesh
    private Light spotlight; // Reference to the Spotlight component

    void Start()
    {
        // Get the Spotlight component attached to this GameObject
        spotlight = GetComponent<Light>();
        FOV = GetComponentInParent<FieldOfView>();
        spotlight.range = FOV.radius;
        spotlight.spotAngle = FOV.angle;

    }
}
