using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLatticeScript : MonoBehaviour
{
    // Basis vectors for the lattice
    private Vector3 b1 = new Vector3(0.5f, -0.5f, 0.707f);
    private Vector3 b2 = new Vector3(0.854f, 0.146f, -0.5f);
    private Vector3 b3 = new Vector3(0.146f, 0.854f, 0.5f);

    // Dimensions of the box (change as needed)
    public Vector3 boxMin = new Vector3(-5f, -5f, -5f);
    public Vector3 boxMax = new Vector3(5f, 5f, 5f);

    // Gameobject for the sphere
    public GameObject sphereGameObject;

    // Lattice parameters
    public int latticeSize = 50; // Number of steps in each direction

    void Start()
    {
        // sphereGameObject = GameObject.Find("VertexProto");
        GenerateLattice();
    }

    void GenerateLattice()
    {
        for (int i = -latticeSize; i <= latticeSize; i++)
        {
            for (int j = -latticeSize; j <= latticeSize; j++)
            {
                for (int k = -latticeSize; k <= latticeSize; k++)
                {
                    // Calculate the lattice point
                    Vector3 point = i * b1 + j * b2 + k * b3;

                    // Check if the point is within the bounding box
                    if (IsWithinBox(point))
                    {
                        // Check if the point lies on a box face
                        if (IsOnBoxFace(point))
                        {
                            // Instantiate a sphere at the lattice point
                            Instantiate(sphereGameObject, point, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    bool IsWithinBox(Vector3 point)
    {
        return (point.x >= boxMin.x && point.x <= boxMax.x &&
                point.y >= boxMin.y && point.y <= boxMax.y &&
                point.z >= boxMin.z && point.z <= boxMax.z);
    }

    bool IsOnBoxFace(Vector3 point)
    {
        // Check if the point is on any of the box faces
        return (Mathf.Approximately(point.x, boxMin.x) || Mathf.Approximately(point.x, boxMax.x) ||
                Mathf.Approximately(point.y, boxMin.y) || Mathf.Approximately(point.y, boxMax.y) ||
                Mathf.Approximately(point.z, boxMin.z) || Mathf.Approximately(point.z, boxMax.z));
    }
}
