using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    //wander variables
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;

    //cohesion variables
    public float cohesionRadius;
    public float cohesionPriority;

    //alignment variables
    public float alignmentRadius;
    public float alignmentPriority;

    //separation variables
    public float separationRadius;
    public float separationPriority;

    //avoidance variables
    public float avoidanceRadius;
    public float avoidancePriority;
}
