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

    public camRotation camRot;

    public GameObject enemyReference;
    
    // Start is called before the first frame update
    void Start()
    {
        Lens = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            RaycastHit hit;

            Vector3 directionToTarget = (col.transform.position - Lens.transform.position).normalized;

            if (Physics.Raycast(Lens.transform.position, directionToTarget, out hit, 1000, detection))
            {
                if (hit.collider.tag == "Player")
                {
                    Lens.GetComponentInParent<MeshRenderer>().material = detect;
                    camRot.detected = true;
                    enemyReference.GetComponent<EnemyMovement>().MoveToCameraPos(hit.transform.position);
                }
                else
                {
                    Lens.GetComponentInParent<MeshRenderer>().material = none;
                    camRot.detected = false;

                }
            }
            else
            {
                Lens.GetComponentInParent<MeshRenderer>().material = none;
                camRot.detected = false;

            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Lens.GetComponentInParent<MeshRenderer>().material = none;
            camRot.detected = false;
        }
    }
}
