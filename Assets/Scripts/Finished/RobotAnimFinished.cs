using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimFinished : MonoBehaviour
{

    public List<Transform> Legs;

    float offset;

    // Store the current y position of the body relative to the feet as the offset
    private void Start()
    {
        offset = transform.position.y - AverageY();
    }

    // Move to the average y position of the legs, plus an offset
    private void LateUpdate()
    {
        transform.position += Vector3.up * (offset + AverageY() - transform.position.y);
    }

    // Returns the average y position of all foot transforms
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
