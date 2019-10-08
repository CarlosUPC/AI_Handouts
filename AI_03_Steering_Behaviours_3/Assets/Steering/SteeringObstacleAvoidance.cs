using UnityEngine;
using System.Collections;

[System.Serializable]
public class RayClass : System.Object
{
    public Vector3 direction = Vector3.forward;
    public float max_distance = 3.0F;
}

public class SteeringObstacleAvoidance : MonoBehaviour {

	public LayerMask mask;
	public float avoid_distance = 5.0f;
    
    public RayClass[] rays;

    Move move;
	SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
		seek = GetComponent<SteeringSeek>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)
       
        float current_angle = Mathf.Atan2(move.movement_vel.x, move.movement_vel.z) * Mathf.Rad2Deg;
        RaycastHit hit;
        
        foreach (RayClass ray in rays)
        {
            Quaternion q = Quaternion.AngleAxis(current_angle, Vector3.up);

            if (Physics.Raycast(new Vector3(transform.position.x, 1.0f, transform.position.z), q * ray.direction.normalized, out hit, ray.max_distance, mask) == true)
            {
                Debug.DrawRay(move.transform.position, ray.direction * ray.max_distance, Color.green);
                Vector3 escape_vector = hit.point + hit.normal * avoid_distance;

                seek.Steer(escape_vector);
            }
            else
            {
                Debug.DrawRay(move.transform.position, (q * ray.direction.normalized) * ray.max_distance, Color.red);
            }
        }


        //float angle = Mathf.Atan2(move.movement_vel.x, move.movement_vel.z);
        //Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        //foreach (my_ray ray in rays)
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(new Vector3(transform.position.x, 1.0f, transform.position.z), q * ray.direction.normalized, out hit, ray.length, mask) == true)
        //        seek.Steer(new Vector3(hit.point.x, transform.position.y, hit.point.z) + hit.normal * avoid_distance);
        //}


    }

    void OnDrawGizmosSelected()
    {
        if (move && this.isActiveAndEnabled)
        {
            Gizmos.color = Color.red;
            float angle = Mathf.Atan2(move.movement_vel.x, move.movement_vel.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)

            //foreach (my_ray ray in rays)
            //    Gizmos.DrawLine(transform.position, transform.position + (q * ray.direction.normalized) * ray.length);

        }
    }
          
}
