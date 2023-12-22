using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleCollider : MonoBehaviour
{
    void Start()
    {
        // Add Polygon Collider 2D if not already added
        PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        // Define rectangle vertices
        Vector2[] rectangleVertices = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        // Set the collider points
        polygonCollider.SetPath(0, rectangleVertices);
    }
}
