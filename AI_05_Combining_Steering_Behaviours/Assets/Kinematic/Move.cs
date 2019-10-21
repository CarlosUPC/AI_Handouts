using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_speed = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_speed = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

    // TODO 2: Add an array of Vector3 for storing the result in every priority level
    private Vector3[] linear_movement = new Vector3[6];
    private float[] angular_movement = new float[6];



    [Header("-------- Read Only --------")]
    public Vector3 current_velocity = Vector3.zero;
	public float current_rotation_speed = 0.0f; // degrees

	// Methods for behaviours to set / add velocities
	public void SetMovementVelocity (Vector3 velocity) 
	{
        current_velocity = velocity;
	}

	public void AccelerateMovement (Vector3 acceleration, int priority) 
	{
        linear_movement[priority] += acceleration;
	}

	public void SetRotationVelocity (float rotation_speed) 
	{
        current_rotation_speed = rotation_speed;
	}

	public void AccelerateRotation (float rotation_acceleration, int priority) 
	{
        angular_movement[priority] += rotation_acceleration;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO 3: 
        // On Update() now we need to evaluate every priority level
        // If it is > 0.0f it means activity, we use it to modify our final velocity
        // Skip the rest of priorities
        // Proceed with the rest of the code(cap velocity …)
        // Put the inspector in Debug mode to see the changes in private variables

        for (int i = linear_movement.Length-1; i >= 0; i--)
        {
            if (!Mathf.Approximately(linear_movement[i].magnitude, 0.0F))
            {
                current_velocity += linear_movement[i];
                Debug.Log("Priority selected: " + i);
                break;
            }
        }

        for (int i = 0; i < angular_movement.Length; i++)
        {
            if (!Mathf.Approximately(angular_movement[i], 0.0F))
            {
                current_rotation_speed += angular_movement[i];
                Debug.Log("Priority selected: " + i);
                break;
            }
        }


        // cap velocity
        if (current_velocity.magnitude > max_mov_speed)
		{
            current_velocity = current_velocity.normalized * max_mov_speed;
		}

        // cap rotation
        current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

		// rotate the arrow
		float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = current_velocity.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += current_velocity * Time.deltaTime;

        // TODO 2: At the end of the Update, put all the values to zero
        for (int i = linear_movement.Length - 1; i >= 0; i--)
        {
            linear_movement[i] = Vector3.zero;
        }
        // TODO 2: Repeat the same process with angular velocity calculations
        for (int i = angular_movement.Length - 1; i >= 0; i--)
        {
            angular_movement[i] = 0.0f;
        }
    }
}
