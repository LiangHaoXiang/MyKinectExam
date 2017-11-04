using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCubeMovement : BaseObjectMovement
{
    private MyBaseGestureListener gestureListener;
    //public GameObject myCube;

    private WholeMove wholeMoveInit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start () {

        base.Start();

        gestureListener = MyBaseGestureListener.Instance;

        wholeMoveInit = WholeMove.Instance;
    }

    protected override void Update ()
    {
        //if (transform.parent.transform.position != wholeMoveInit.points[1].position)
        //{
        //    return;
        //}
        //else
        {
            if (actionState != ActionState.Delay)
            {
                if (!isSpinning)
                {
                    if (gestureListener.IsSwipeUp())
                    {
                        RotateUp();
                    }
                    else if (gestureListener.IsSwipeDown())
                    {
                        RotateDown();
                    }
                    else if (gestureListener.IsSwipeLeft())
                    {
                        RotateLeft();
                    }
                    else if (gestureListener.IsSwipeRight())
                    {
                        RotateRight();
                    }
                }
            }
            base.Update();
        }
	}


    //public bool SetGestureListenerActive(bool value)
    //{
    //    if (value == true)
    //        gestureListener.SetActive(true);
    //    else
    //        gestureListener.SetActive(false);
    //    return value;
    //}
}
