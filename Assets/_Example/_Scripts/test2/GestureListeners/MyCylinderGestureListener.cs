using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCylinderGestureListener : MyBaseGestureListener
{
    private static MyCylinderGestureListener instance = null;

    public static MyCylinderGestureListener Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

}
