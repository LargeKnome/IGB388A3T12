using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Detection : MonoBehaviour
{
    public Material detect;
    public Material none;
    string playerTag;

    public LayerMask detection;
    Transform Lens;
    // Start is called before the first frame update
    void Start()
    {
        Lens = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            RaycastHit hit;

            Vector3 directionToTarget = (col.transform.position - Lens.transform.position).normalized;

            Debug.DrawRay(Lens.transform.position, directionToTarget);
            if (Physics.Raycast(Lens.transform.position, directionToTarget, out hit, 1000, detection))
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.tag == playerTag)
                {
                    Lens.GetComponentInParent<MeshRenderer>().material = detect;
                }
                else
                {
                    Lens.GetComponentInParent<MeshRenderer>().material = none;
                }
            }
            else
            {
                Lens.GetComponentInParent<MeshRenderer>().material = none;
            }
        }
        else
        {
            Lens.GetComponentInParent<MeshRenderer>().material = none;
        }
    }
}
