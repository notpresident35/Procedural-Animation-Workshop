using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic
{
    public static float In(float k)
    {
        return k * k * k;
    }

    public static float Out(float k)
    {
        return 1f + ((k -= 1f) * k * k);
    }

    public static float InOut(float k)
    {
        if ((k *= 2f) < 1f) return 0.5f * k * k * k;
        return 0.5f * ((k -= 2f) * k * k + 2f);
    }
}

public class Leg : MonoBehaviour
{
    public float StepDist;
    public float StepSpeed;
    public Transform SuggestedTarget;
    public DitzelGames.FastIK.FastIKFabric IK;
    public Leg OppositeLeg;

    Vector3 prevPos;
    Quaternion prevRot;
    public float stepInterpolator = 1;

    private void Start()
    {
        // initialize
        prevPos = IK.Target.position;
        prevRot = IK.Target.rotation;
    }

    private void Update()
    {
        // Update interpolator with time
        stepInterpolator = Mathf.Clamp01(stepInterpolator + Time.deltaTime * StepSpeed);

        // Move suggested target up if objects are detected
        RaycastHit hitInfo;
        if (Physics.Raycast(SuggestedTarget.position + new Vector3(0, 100, 0), Vector3.down, out hitInfo, 500))
        {
            SuggestedTarget.transform.position = hitInfo.point;
        }

        // If the leg's solver target gets far enough away from its intended target and can currently step, take a "step"
        float dist = (SuggestedTarget.position - IK.Target.position).magnitude;
        if (dist > StepDist && stepInterpolator >= 1 && OppositeLeg.stepInterpolator >= 1)
        {
            HandleStep();
        }

        // If currentlyStepping
        if (stepInterpolator < 1)
        {
            // Smoothly move target to intended target
            IK.Target.position = Vector3.Lerp(prevPos, SuggestedTarget.position, Cubic.InOut(stepInterpolator));
            IK.Target.rotation = Quaternion.Lerp(prevRot, SuggestedTarget.rotation, Cubic.InOut(stepInterpolator));
        }
    }

    // "Take a step" by updating the current solver and history positions/rotations
    public void HandleStep()
    {
        stepInterpolator = 0;
        prevPos = IK.Target.position;
        prevRot = IK.Target.rotation;
    }
}
