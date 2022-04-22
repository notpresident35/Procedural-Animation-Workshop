using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegFinished : MonoBehaviour
{
    public float StepDist;
    public float StepSpeed;
    public Transform SuggestedTarget;
    public DitzelGames.FastIK.FastIKFabric IK;
    public LegFinished OppositeLeg;

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
        RaycastHit hitInfo;
        if (Physics.Raycast(SuggestedTarget.position + new Vector3(0, 100, 0), Vector3.down, out hitInfo, 500))
        {
            SuggestedTarget.transform.position = hitInfo.point;
        }

        // If the leg's solver target gets far enough away from its intended target and can currently step, take a "step"
        float dist = (SuggestedTarget.position - IK.Target.position).magnitude;
        if (dist > StepDist && stepInterpolator >= 1 && OppositeLeg.stepInterpolator >= 1)
        {
            BeginStep();
        }

        // If currently stepping, smoothly move IK target to intended target
        if (stepInterpolator < 1)
        {
            IK.Target.position = Vector3.Lerp(prevPos, SuggestedTarget.position, Cubic.InOut(stepInterpolator));
            IK.Target.rotation = Quaternion.Lerp(prevRot, SuggestedTarget.rotation, Cubic.InOut(stepInterpolator));
        }
    }

    // Allow the leg to "take a step" by resetting the step interpolator and storing the IK's current position/rotation
    public void BeginStep()
    {
        stepInterpolator = 0;
        prevPos = IK.Target.position;
        prevRot = IK.Target.rotation;
    }
}
