using UnityEngine;
using UnityEngine.UI;

public class ResetSphere : MonoBehaviour
{
    public Rigidbody fallingSphere; // Assign the sphere’s Rigidbody here

    private LineRenderer[] edgeLines = new LineRenderer[4]; // Array to hold LineRenderers for edges
    public Vector3 boxSize = new Vector3(8, 8, 8); // Size of the box, centered at origin

    void Start()
    {
        // Draw the edges on the front face of the box
        DrawBoxFrontEdges();
    }

    public void ResetAndLaunch()
    {
        if (fallingSphere != null)
        {
            // 1. Set velocity to zero
            fallingSphere.linearVelocity = Vector3.zero;
            fallingSphere.angularVelocity = Vector3.zero;

            // 2. Move to a random position within an 8x8x8 box centered at the origin
            Vector3 randomPosition = new Vector3(
                Random.Range(-4f, 4f),
                Random.Range(-4f, 4f),
                Random.Range(-4f, 4f)
            );
            fallingSphere.position = randomPosition;

            // 3. Apply an impulse in a random direction
            Vector3 randomDirection = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
            fallingSphere.AddForce(randomDirection, ForceMode.Impulse);
        }
    }

    void DrawBoxFrontEdges()
    {
        // Define the corner points of the front face
        Vector3 halfSize = boxSize / 2;
        Vector3[] corners = new Vector3[4]
        {
            new Vector3(-halfSize.x,  halfSize.y, -halfSize.z), // Top-left corner
            new Vector3( halfSize.x,  halfSize.y, -halfSize.z), // Top-right corner
            new Vector3( halfSize.x, -halfSize.y, -halfSize.z), // Bottom-right corner
            new Vector3(-halfSize.x, -halfSize.y, -halfSize.z)  // Bottom-left corner
        };

        // Create 4 line renderers to draw edges between the corners
        for (int i = 0; i < 4; i++)
        {
            GameObject lineObj = new GameObject("EdgeLine" + i);
            edgeLines[i] = lineObj.AddComponent<LineRenderer>();
            edgeLines[i].positionCount = 2;
            edgeLines[i].startWidth = 0.05f;
            edgeLines[i].endWidth = 0.05f;
            edgeLines[i].material = new Material(Shader.Find("Sprites/Default")); // Basic material for visibility
            edgeLines[i].startColor = Color.red;
            edgeLines[i].endColor = Color.red;

            // Set positions to draw the edge
            edgeLines[i].SetPosition(0, corners[i]);
            edgeLines[i].SetPosition(1, corners[(i + 1) % 4]); // Connect to the next corner
        }
    }
}
