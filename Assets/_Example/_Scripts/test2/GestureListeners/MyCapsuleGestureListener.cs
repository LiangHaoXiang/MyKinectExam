using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCapsuleGestureListener : MyBaseGestureListener
{
    private static MyCapsuleGestureListener instance = null;

    public static MyCapsuleGestureListener Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }
}
