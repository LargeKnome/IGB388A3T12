using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotation : MonoBehaviour
{
    Transform cam;

    bool startNext = true;
    public bool rotateRight;
    public float yaw;
    public float pitch;

    public float rotationSec;

    public float rotationFlip;

    public bool detected = false;

    private GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        cam = transform.GetChild(0);
        cam.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        SetUpStartRotation();
    }
    private void Update()
    {
        if (!detected)
        {
            if (startNext && rotateRight)
            {
                StartCoroutine(Rotate(yaw, rotationSec));
            }
            else if (startNext && !rotateRight)
            {
                StartCoroutine(Rotate(-yaw, rotationSec));
            }
        }
        else
        {
            transform.LookAt(playerRef.transform.position);
        }
    }

    // Update is called once per frame
    IEnumerator Rotate(float yaw, float duration)
    {
        startNext = false;

        Quaternion intialRotation = transform.rotation;

        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = intialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotationFlip);
        startNext = true;
        rotateRight = !rotateRight;

    }
    void SetUpStartRotation()
    {
        if (rotateRight)
        {
            transform.localRotation = Quaternion.AngleAxis(-yaw / 2, Vector3.up);
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(yaw / 2, Vector3.up);
        }
    }
}
