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

    public bool detected;

    private GameObject playerRef;

    private bool Resetcam = false;

    private Quaternion camRot;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        cam = transform.GetChild(0);
        camRot = cam.rotation;
        cam.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        startRotation = Quaternion.Euler(Vector3.up * yaw);
        Debug.Log(startRotation.y);
        endRotation = Quaternion.Euler(Vector3.up * -yaw);
        targetRotation = endRotation;
/*        SetUpStartRotation();
*/    }
    private void Update()
    {
        if (!detected)
        {
            if (Resetcam)
            {
                startNext = true;
                rotateRight = false;
                cam.rotation = camRot;
                cam.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
/*                SetUpStartRotation();
*/                Resetcam = false;
            }
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSec * Time.deltaTime);
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                if (targetRotation == endRotation)
                {
                    targetRotation = startRotation;
                }
                else
                {
                    targetRotation = endRotation;
                }
            }
            /*            if (startNext && rotateRight)
                        {
                            StartCoroutine(Rotate(yaw, rotationSec));
                        }
                        else if (startNext && !rotateRight)
                        {
                            StartCoroutine(Rotate(-yaw, rotationSec));
                        }*/
        }
        else
        {
            cam.transform.LookAt(playerRef.transform.position);
            Resetcam = true;
        }
    }

    // Update is called once per frame
    IEnumerator Rotate(float yaw, float duration)
    {
        startNext = false;

        Quaternion intialRotation = transform.rotation;

        float timer = 0f;

        while (timer < duration)
        {
            if (!detected)
            {
                timer += Time.deltaTime;
                transform.rotation = intialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
                yield return null;
            }
            else
            {
                yield break;
            }
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
