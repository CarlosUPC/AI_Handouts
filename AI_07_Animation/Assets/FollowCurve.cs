using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

//[RequireComponent(typeof(BGCcMath))]
public class FollowCurve : MonoBehaviour
{
    public float ratio_increment = 0.05f;
    float current_ratio = 0.0f;
    Vector3 closest_point;
    public BGCcMath path;

    // Start is called before the first frame update
    void Start()
    {
        float distance;
        closest_point = path.CalcPositionByClosestPoint(transform.position, out distance);

        current_ratio = distance / path.GetDistance();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = closest_point - transform.position;

        if (direction.magnitude < 0.05f)
        {
            current_ratio += ratio_increment;

            if (current_ratio > 1.0f)
                current_ratio = 0.0f;

            closest_point = path.CalcPositionByDistanceRatio(current_ratio);
        }
        else
            this.transform.position += direction.normalized * 5 * Time.deltaTime;
             //this.transform.Translate(direction.normalized * 5 * Time.deltaTime);
    }
}
