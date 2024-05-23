using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private NavMeshAgent agent;

    public bool playerDetected;

    private Vector3 hunterOrigin;

    public int rayCount = 36;

    public int resolution = 50;     // Number of points used to draw the FOV


    private Mesh mesh;

    public MeshFilter detection;

    public Material NotDetected;
    public Material Detected;
        
    private void Start()
    {
        mesh = new Mesh();
        Debug.Log(gameObject.name);
        detection.mesh = mesh;


        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        agent = GetComponent<NavMeshAgent>();
        hunterOrigin = transform.position;

    }
    private void Update()
    {
        DrawFOV();
/*        if (canSeePlayer & !playerDetected)
        {
            transform.LookAt(playerRef.transform.position);
            StartCoroutine(DetectPlayer());
        }
        else if (!canSeePlayer)
        {
            playerDetected = false;
        }
        if (playerDetected)
        {
            GetComponent<EnemyMovement>().MoveTo();
        }*/
    }
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0 )
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    detection.GetComponent<MeshRenderer>().material = Detected;
                    canSeePlayer = true;
/*                    GetComponent<EnemyMovement>().camAlert = false;
*/                }
                else
                {
                    detection.GetComponent<MeshRenderer>().material = NotDetected;
                    canSeePlayer = false;
                }
            }
            else
            {
                detection.GetComponent<MeshRenderer>().material = NotDetected;
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            detection.GetComponent<MeshRenderer>().material = NotDetected; 
            canSeePlayer = false;
        }
    }

    public void DetectingPlayer()
    {
        StartCoroutine(DetectPlayer());
    }

    private IEnumerator DetectPlayer()
    {
        float elaspedTime = 0f;
        while (elaspedTime < 5f)
        {
            if (!canSeePlayer)
            {
                yield break;
            }
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        playerDetected = true;
        yield return null;
/*        WaitForSeconds wait = new WaitForSeconds(5f);
        yield return wait;
        if (canSeePlayer)
        {
            playerDetected = true;
        }
        yield return null;*/
    }
    void DrawFOV()
    {
        // Define arrays to hold vertices and triangles of the mesh
        Vector3[] vertices = new Vector3[resolution + 1]; // Vertices array with an additional point for the origin
        int[] triangles = new int[resolution * 3]; // Triangles array

        // Set the origin vertex to the center of the FOV
        vertices[0] = Vector3.zero;

        // Calculate the angle step between each vertex
        float angleStep = angle / resolution;

        // Start with the initial angle at the left edge of the FOV
        float currentAngle = -angle / 2f;

        // Loop through each vertex of the FOV
        for (int i = 1; i <= resolution; i++)
        {
            // Convert the current angle to radians
            float angleRad = Mathf.Deg2Rad * currentAngle;

            // Calculate the position of the vertex on the circumference of the FOV
            vertices[i] = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad)) * radius;

            // Increment the angle for the next vertex
            currentAngle += angleStep;
        }

        // Define triangles to form the mesh
        for (int i = 0, j = 1; i < triangles.Length; i += 3, j++)
        {
            // Define each triangle
            triangles[i] = 0;           // Origin vertex
            triangles[i + 1] = j;       // Current vertex
            triangles[i + 2] = j + 1;   // Next vertex
        }

        // Connect the last vertex to the second vertex to close the loop
        triangles[triangles.Length - 1] = 1;

        // Clear the mesh and assign vertices and triangles
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals to ensure correct lighting
        mesh.RecalculateNormals();
    }
}
