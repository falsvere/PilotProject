using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBarierControll : BaseBarier
{
    [SerializeField] GameObject triangle;

    void LateUpdate()
    {
        Vector3 trianglePosition = triangle.transform.position;
        trianglePosition.y += 0.320f;
        transform.position = trianglePosition;
    }
}
