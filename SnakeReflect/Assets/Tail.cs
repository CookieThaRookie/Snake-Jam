using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour
{
    public int maxPoints = 25;
    public float pointSpacing = .1f; // How far to move before we fraw new point
    public Transform head;
    
    List<Vector2> points;

    LineRenderer line;
    EdgeCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();

        points = new List<Vector2>();

        SetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        // We only want to set a new point if we have moved far enough away
        if (Vector3.Distance(points.Last(), head.position) > pointSpacing)
            SetPoint();

        if (points.Count > maxPoints)
        {
            points.RemoveAt(0);
        }
    }

    void SetPoint()
    {
        /* OLD somewhat working code
        points.Add(head.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, head.position);
        */

        // Add points to edge collider
        col.points = points.ToArray<Vector2>();

        points.Add(head.position);

        line.positionCount = points.Count;
        
        // We place all the vectors in points into the linerenderer
        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i]);
        }
    }
}
