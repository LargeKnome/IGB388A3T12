using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] private Transform playerLeftHand;
    [SerializeField] private Transform playerRightHand;

    [SerializeField] private GameObject rightObject;
    [SerializeField] private GameObject leftObject;
    
    private bool rightFistOn;
    private bool leftFistOn;
    // Start is called before the first frame update
    void Start()
    {
        rightObject.SetActive(false);
        rightFistOn = false;
        leftObject.SetActive(false);
        leftFistOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && !rightFistOn)
        {
            rightFistOn = true;
            rightObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            rightFistOn = false;
            rightObject.SetActive(false);
        }
        if (rightFistOn)
        {
            rightObject.transform.position = playerRightHand.position;
        }
        
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && !leftFistOn)
        {
            leftFistOn = true;
            leftObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            leftFistOn = false;
            leftObject.SetActive(false);
        }
        if (leftFistOn)
        {
            leftObject.transform.position = playerLeftHand.position;
        }
    }
}
