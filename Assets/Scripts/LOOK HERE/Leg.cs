using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public float StepDist;
    public float StepSpeed;
    public Transform SuggestedTarget;
    public DitzelGames.FastIK.FastIKFabric IK;
    public Leg OppositeLeg;

    Vector3 prevPos;
    Quaternion prevRot;
    [HideInInspector]
    public float stepInterpolator = 1;

    private void Start()
    {
        // initialize
        prevPos = IK.Target.position;
        prevRot = IK.Target.rotation;
    }

    private void Update()
    {
        // Update interpolator
        stepInterpolator = Mathf.Clamp01(stepInterpolator + Time.deltaTime * StepSpeed);

        // Raycast to check for surfaces, and move the suggested target onto them if any found

        // If the leg's solver target gets far enough away from its intended target and can currently step, take a "step"

        // If currently stepping, smoothly move IK target to intended target

    }

    // Allow the leg to "take a step" by resetting the step interpolator and storing the IK's current position/rotation
    public void BeginStep()
    {
        
    }
}
