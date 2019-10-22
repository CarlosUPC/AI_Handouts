using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : SteeringAbstract
{

	Move move;
	SteeringSeek seek;

    public float ratio_increment = 0.05f;
    public float min_distance = 0.5f;
    float current_ratio = 0.0f;
    
    public BGCcMath path;
    Vector3 closest_point;
   
    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point from the tank to the curve

        float distance;
        closest_point = path.CalcPositionByClosestPoint(transform.position, out distance); 

        current_ratio = distance / path.GetDistance(); // Distance from initial point into distance ratio range[0,1]
    }
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        Vector3 position = closest_point - transform.position;

        if (position.magnitude < min_distance) // min distance to closest point
        {
            current_ratio += ratio_increment; //increment ratio distance to reach 1 from range [0,1]

            if (current_ratio > 1.0f) // if ratio distance reach 1, it becomes 0 to start again from initial point
                 current_ratio = 0.0f;
          
            closest_point = path.CalcPositionByDistanceRatio(current_ratio); // Returns the position due current ratio
        }
        else
            seek.Steer(closest_point); 
        


    }


	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
