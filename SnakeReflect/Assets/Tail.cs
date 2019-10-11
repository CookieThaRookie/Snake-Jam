using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tail : MonoBehaviour
{
    public float pointSpacing = .1f; // How far to move before we fraw new point
    public Transform head;

    List<Vector2> points;

    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        points = new List<Vector2>();

        SetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        // We only want to set a new point if we have moved far enough away
        if (Vector3.Distance(points.Last(), head.position) > pointSpacing)
            SetPoint();
    }

    void SetPoint()
    {
        points.Add(head.position);

        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, head.position);
    }
}
