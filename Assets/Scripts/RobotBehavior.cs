using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    public float MoveSpeed;
    public float RotDamping;
    public float FollowDist;
    public float StoppingDist;

    Plane plane = new Plane(Vector3.up, 0);

    public Transform mouseGizmo;

    void Update() {

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            mouseGizmo.position = ray.GetPoint(distance);
        }

        float dist = (transform.position - mouseGizmo.position).magnitude;

        // Store the other object's position in a temporary variable
        var temp = new Vector3(mouseGizmo.position.x, 0, mouseGizmo.position.z);
        temp -= transform.position;
        temp.y = 0;

        float angle = Mathf.Atan2(temp.x, temp.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), Cubic.In(Mathf.Clamp01(dist / FollowDist - StoppingDist)));

        transform.position += transform.forward * Time.deltaTime * MoveSpeed * Cubic.In(Mathf.Clamp01(dist / FollowDist - StoppingDist));
    }
}
