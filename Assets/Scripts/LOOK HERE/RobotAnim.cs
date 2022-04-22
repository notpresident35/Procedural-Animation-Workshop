using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour
{

    public List<Transform> Legs;

    float offset;

    // Store the current y position of the body relative to the feet as the offset
    private void Start()
    {
        
    }

    // Move to the average y position of the legs, plus an offset
    private void LateUpdate()
    {
        
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
