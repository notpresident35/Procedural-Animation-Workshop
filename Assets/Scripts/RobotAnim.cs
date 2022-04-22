using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour
{

    public List<Transform> Legs;

    float offset;

    private void Start()
    {
        offset = transform.position.y - AverageY();
    }

    private void LateUpdate()
    {
        transform.position += Vector3.up * (offset + AverageY() - transform.position.y);
        // transform.position += Vector3.up * (-transform.position.y) + Vector3.up * (offset + AverageY());
    }

    float AverageY()
    {
        float avg = 0;
        foreach (Transform leg in Legs)
        {
            avg += leg.position.y;
        }
        avg /= Legs.Count;
        return avg;
    }
}
