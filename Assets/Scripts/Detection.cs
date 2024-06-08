using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Detection : MonoBehaviour
{
    public Light light;
    public Material detectSpotlight;
    public Material noneSpotlightLight;
    string playerTag;

    public LayerMask obstruction;

    public LayerMask detection;
    Transform Lens;

    public camRotation camRot;

    private GameObject[] enemyReferences;

    private GameObject player;

    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.color = noneSpotlightLight.color;
        enemyReferences = GameObject.FindGameObjectsWithTag("Enemy");
        Lens = transform.parent.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(player.transform.position, Lens.transform.position);

    }
    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, Lens.transform.position);

    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            RaycastHit hit;

            Vector3 directionToTarget = (player.transform.position - Lens.transform.position).normalized;
            Debug.DrawRay(Lens.transform.position, directionToTarget * distance, Color.red); // Draw the ray
            if (Physics.Raycast(Lens.transform.position, directionToTarget, out hit, distance, obstruction))
            {
                GetComponent<MeshRenderer>().material = noneSpotlightLight;
                light.color = noneSpotlightLight.color;
                camRot.detected = false;
            }
            else if (Physics.Raycast(Lens.transform.position, directionToTarget, out hit, distance, detection))
            {
                if (hit.collider.tag == "Player")
                {
                    GetComponent<MeshRenderer>().material = detectSpotlight;
                    light.color = detectSpotlight.color;
                    camRot.detected = true;
                    GameObject closestEnemy = FindClosestEnemy(hit);
                    if (closestEnemy != null)
                    {
                        closestEnemy.GetComponent<Hunter>().MoveToCameraPos(hit.transform.position);
                    }

                }
                else
                {
                    GetComponent<MeshRenderer>().material = noneSpotlightLight;
                    light.color = noneSpotlightLight.color;
                    camRot.detected = false;

                }
            }
        }
    }
    private GameObject FindClosestEnemy(RaycastHit hit)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in enemyReferences)
        {
            float distance = Vector3.Distance(hit.transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material = noneSpotlightLight;
            light.color = noneSpotlightLight.color;
            camRot.detected = false;
        }
    }
}
