using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeMove : BaseObjectMovement
{
    private MyBaseGestureListener gestureListener;

    public Transform[] points;

    int Cube = 0;
    int Cylinder = 1;
    int Capsule = 2;

    public GameObject[] objs;

    private bool isTurningPage;

    private static WholeMove instance = null;
    public static WholeMove Instance { get { return instance; } }

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }

    protected override void Start ()
    {
        isTurningPage = false;
        gestureListener = MyBaseGestureListener.Instance;
	}

    protected override void Update ()
    {
        if (actionState == ActionState.Wait)
        {
            if (!isTurningPage)
            {
                if (gestureListener.IsTurnPageRight())
                {
                    MoveLeft();
                }
                if (gestureListener.IsTurnPageLeft())
                {
                    MoveRight();
                }
            }
        }
        for(int i = 0; i < objs.Length; i++)
        {
            if (objs[i].transform.position.x == points[1].position.x)
                objs[i].transform.GetChild(1).gameObject.SetActive(true);
            else
                objs[i].transform.GetChild(1).gameObject.SetActive(false);
        }
	}
    /// <summary>
    /// UI按钮时，页面往右翻，手势相反
    /// </summary>
    public void MoveRight()
    {
        actionState = ActionState.Move; //改变为移动状态
        for (int i = 0; i < points.Length; i++)
        {
            if (objs[i].transform.position == points[0].position)
            {
                objs[i].transform.position = points[points.Length - 1].position;
                continue;
            }
            iTween.MoveBy(objs[i], iTween.Hash("time", 0.5f, "x", -8.0f, "easetype", iTween.EaseType.linear));
        } 

        //0.5秒后，由移动状态改为延迟状态，然后再过fromDelayToWaitTime时间后，变为等待状态
        StartCoroutine(ChangeActionState(0.5f, fromDelayToWaitTime));
    }
    /// <summary>
    /// UI按钮时，页面往左翻，手势相反
    /// </summary>
    public void MoveLeft()
    {
        actionState = ActionState.Move;
        for (int i = 0; i < points.Length; i++)
        {
            if (objs[i].transform.position == points[points.Length - 1].position)
            {
                objs[i].transform.position = points[0].position;
                continue;
            }
            iTween.MoveBy(objs[i], iTween.Hash("time", 0.5f, "x", 8.0f, "easetype", iTween.EaseType.linear));
        }
        StartCoroutine(ChangeActionState(0.5f, fromDelayToWaitTime));
    }
}
