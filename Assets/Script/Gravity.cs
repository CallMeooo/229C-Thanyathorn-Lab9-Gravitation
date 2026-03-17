using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    //List of attractable objects
    public static List<Gravity> otherObjectList;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectList == null) 
        {
            otherObjectList = new List<Gravity>();
        }

        otherObjectList.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectList)
        {
            if (obj != this)
            {
                AttractForce(obj);
            }
        }
    }
    void AttractForce(Gravity other)
    { 
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;

        if (distance == 0f) { return;}

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}
