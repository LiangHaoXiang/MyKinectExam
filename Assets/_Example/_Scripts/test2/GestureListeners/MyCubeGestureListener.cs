using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCubeGestureListener : MyBaseGestureListener
{
    private static MyCubeGestureListener instance = null;

    public static MyCubeGestureListener Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }
}
