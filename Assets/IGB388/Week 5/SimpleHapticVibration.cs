using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHapticVibrationManager
{
    public static OVRManager manager = null;

    public static void VibrateController (float duration, float strength, OVRInput.Controller controller)
    {
        if (manager == null) manager = GameObject.FindObjectOfType<OVRManager>();
        manager.StartCoroutine(VibrateControllerCoroutine (duration, strength, controller));
    }

    private static IEnumerator VibrateControllerCoroutine (float duration, float strength, OVRInput.Controller controller)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            OVRInput.SetControllerVibration(0.1f, strength, controller);
            yield return null;
        }
        OVRInput.SetControllerVibration(0.1f, 0, controller);
    }
}
