using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCapsuleMovement : BaseObjectMovement {

    private MyBaseGestureListener gestureListener;

    private WholeMove wholeMoveInit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {

        base.Start();

        gestureListener = MyBaseGestureListener.Instance;
        wholeMoveInit = WholeMove.Instance;
    }

    protected override void Update()
    {
        //if (transform.parent.transform.position != wholeMoveInit.points[1].position)
        //    return;
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
