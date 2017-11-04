using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeMoveGestureListener : MyBaseGestureListener
{

    private static WholeMoveGestureListener instance = null;

    public static WholeMoveGestureListener Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

}
