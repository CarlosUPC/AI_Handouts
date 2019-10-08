using UnityEngine;
using System.Collections;

public class SteeringSeparation : MonoBehaviour {

	public LayerMask mask;
	public float search_radius = 5.0f;
	public AnimationCurve strength;
    private Vector3 prev_pos;
    public float avoidance = 3.0f;
    SteeringSeek seek;
    Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
        prev_pos = transform.position;
        seek = GetComponent<SteeringSeek>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        // 2- For each of them calculate a escape vector using the AnimationCurve
        // 3- Sum up all vectors and trim down to maximum acceleration

        Collider[] colliders = Physics.OverlapSphere(transform.position, search_radius, mask);
        Vector3 final = Vector3.zero;
        foreach (Collider col in colliders)
        {
            GameObject go = col.gameObject;

            if (go == gameObject)
                continue;
           
           // prev_pos = transform.position;
            Vector3 diff = transform.position - go.transform.position;
            diff.y = 0.0f;
            float distance = diff.magnitude;
            float acceleration = (1.0f - strength.Evaluate(distance / search_radius)) * move.max_mov_acceleration;

            final += diff.normalized * acceleration;
        }

        float final_strength = final.magnitude;
        if (final_strength > 0.0f)
        {
            if (final_strength > move.max_mov_acceleration)
                final = final.normalized * move.max_mov_acceleration;

            //Vector3 avoid_pos =  this.transform.position += (final * avoidance);
            //seek.Steer(avoid_pos);
           move.AccelerateMovement(final);
        }
     
        
    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
