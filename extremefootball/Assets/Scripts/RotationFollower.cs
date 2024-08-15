using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    public Transform Target;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Target.rotation;
    }
}
