using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectMovement : MonoBehaviour {

    protected bool isSpinning = false;
    protected Quaternion initialRotation;
    protected int stepsToGo = 0;
    protected Vector3 rotationStep;
    protected int spinSpeed = 15;

    public GameObject myGameObject;

    public enum ActionState
    {
        Wait,   //等待用户状态
        Move,   //移动状态
        Delay,  //移动后的延迟状态
    }
    public static ActionState actionState;


    protected float fromDelayToWaitTime;    //延时状态到等待状态的时间
    public static float delayTime;

    protected virtual void Awake()
    {
        actionState = ActionState.Wait;
        fromDelayToWaitTime = 0.5f;
    }

    protected virtual void Start ()
    {
        isSpinning = false;
        initialRotation = myGameObject.transform.rotation;
    }

    protected virtual void Update ()
    {
        if (actionState != ActionState.Delay)
        {
            if (!isSpinning)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    RotateUp();
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    RotateDown();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    RotateLeft();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    RotateRight();
                }
            }

            else
            {
                if (stepsToGo > 0)
                {
                    myGameObject.transform.Rotate(rotationStep, Space.World);
                    stepsToGo--;
                }
                else
                {
                    myGameObject.transform.rotation = Quaternion.Euler(rotationStep * 90f / spinSpeed) * initialRotation;
                    isSpinning = false;
                    StartCoroutine(ChangeActionState(0, fromDelayToWaitTime));
                }
            }
        }


    }


    protected void RotateLeft()
    {
        isSpinning = true;
        actionState = ActionState.Move;
        initialRotation = myGameObject.transform.rotation;
        rotationStep = new Vector3(0, 1 * spinSpeed, 0);
        stepsToGo = 90 / spinSpeed;
    }

    protected void RotateRight()
    {
        isSpinning = true;
        actionState = ActionState.Move;
        initialRotation = myGameObject.transform.rotation;
        rotationStep = new Vector3(0, -1 * spinSpeed, 0);
        stepsToGo = 90 / spinSpeed;
    }

    protected void RotateUp()
    {
        isSpinning = true;
        actionState = ActionState.Move;
        initialRotation = myGameObject.transform.rotation;
        rotationStep = new Vector3(1 * spinSpeed, 0, 0);
        stepsToGo = 90 / spinSpeed;
    }

    protected void RotateDown()
    {
        isSpinning = true;
        actionState = ActionState.Move;
        initialRotation = myGameObject.transform.rotation;
        rotationStep = new Vector3(-1 * spinSpeed, 0, 0);
        stepsToGo = 90 / spinSpeed;
    }

    /// <summary>
    /// 改变状态，延迟的状态一秒后变成等待用户状态
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ChangeActionState(float delayInvokeTime, float delayToWaitTime)
    {
        yield return new WaitForSeconds(delayInvokeTime);
        if (actionState != ActionState.Delay)
        {
            actionState = ActionState.Delay;
            delayTime = delayToWaitTime;
            yield return new WaitForSeconds(delayToWaitTime);
        }
        actionState = ActionState.Wait;
    }
}
