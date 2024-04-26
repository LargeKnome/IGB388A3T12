using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (col.gameObject.tag == "Player")
        {
            RaycastHit hit;

            if (Physics.Raycast(Lens.transform.position, Vector3.forward, out hit, 1000, detection))
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
