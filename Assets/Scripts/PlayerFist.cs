using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFist : MonoBehaviour
{
    enum HandType
    {
        Left,
        Right
    }
    [SerializeField] private HandType handType;
    
    public float GetHandSpeed()
    {
        if (handType == HandType.Left)
        {
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand).magnitude;
        }
        else if (handType == HandType.Right)
        {
            return OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand).magnitude;
        }
        else
        {
            return 0;
        }
    }
}
