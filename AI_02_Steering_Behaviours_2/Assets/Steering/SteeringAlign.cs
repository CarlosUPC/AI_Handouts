using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 7: Very similar to arrive, but using angular velocities
        // Find the desired rotation and accelerate to it
        // Use Vector3.SignedAngle() to find the angle between two directions

        // Orientation we are trying to match

        //----------Marc Garrigó Code ----------//
        float my_orientation = Mathf.Rad2Deg * Mathf.Atan2(transform.forward.x, transform.forward.z);
        float target_orientation = Mathf.Rad2Deg * Mathf.Atan2(move.target.transform.forward.x, move.target.transform.forward.z);
        float diff = Mathf.DeltaAngle(my_orientation, target_orientation); // wrap around PI

        float diff_absolute = Mathf.Abs(diff);

        if (diff_absolute < min_angle)
        {
            move.SetRotationVelocity(0.0f);
            return;
        }

        float ideal_rotation = 0.0f;

        if (diff_absolute > slow_angle)
            ideal_rotation = move.max_rot_speed;
        else
            ideal_rotation = move.max_rot_speed * diff_absolute / slow_angle;

        float angular_acceleration = ideal_rotation / time_to_target;

        if (diff < 0)
            angular_acceleration = -angular_acceleration;

        move.AccelerateRotation(Mathf.Clamp(angular_acceleration, -move.max_rot_acceleration, move.max_rot_acceleration));



        //----------Carlos Code 1----------//
        //Vector3 targetDir = move.target.transform.position - transform.position;
        //Vector3 forward = transform.forward;
        //float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);

        //float angularRotation = angle;
        //move.SetRotationVelocity(angularRotation * 5);


        //----------Carlos Code 2----------//
        //Vector3 targetDir = move.target.transform.position - transform.position;
        //Debug.DrawRay(transform.position, targetDir, Color.green);

        //float angle = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg - 90;
        //Debug.Log("Angle: " + angle);

        //move.AccelerateRotation(angle);

        //Quaternion angleAxis = Quaternion.AngleAxis(-angle, Vector3.up);

        //transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 10);
     
    }
}
