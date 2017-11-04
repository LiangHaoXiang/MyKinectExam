using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBaseGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    private static MyBaseGestureListener instance = null;

    public static MyBaseGestureListener Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    [Tooltip("用于显示手势的相关信息")]
    public GUIText gestureInfo;

    //public GUIText myNewGestureInfo;

    protected bool swipeLeft;
    protected bool swipeRight;
    protected bool swipeUp;
    protected bool swipeDown;

    protected bool turnPageRight;
    protected bool turnPageLeft;

    public bool IsSwipeLeft()
    {
        if (swipeLeft)
        {
            swipeLeft = false;
            return true;
        }
        return false;
    }
    public bool IsSwipeRight()
    {
        if (swipeRight)
        {
            swipeRight = false;
            return true;
        }
        return false;
    }
    public bool IsSwipeUp()
    {
        if (swipeUp)
        {
            swipeUp = false;
            return true;
        }
        return false;
    }
    public bool IsSwipeDown()
    {
        if (swipeDown)
        {
            swipeDown = false;
            return true;
        }
        return false;
    }
    public bool IsTurnPageRight()
    {
        if (turnPageRight)
        {
            turnPageRight = false;
            return true;
        }
        return false;
    }
    public bool IsTurnPageLeft()
    {
        if (turnPageLeft)
        {
            turnPageLeft = false;
            return true;
        }
        return false;
    }

    public virtual bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userId != manager.GetPrimaryUserID()))
            return false;
        //if (gestureInfo != null)
        //{
        //    gestureInfo.GetComponent<GUIText>().text = "手势取消了";
        //}
        return true;
    }

    public virtual bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userId != manager.GetPrimaryUserID()))
            return false;

        if (BaseObjectMovement.actionState == BaseObjectMovement.ActionState.Wait)
        {
            if (gesture == KinectGestures.Gestures.Left)
            {
                swipeLeft = true;
            }
            else if (gesture == KinectGestures.Gestures.Right)
            {
                swipeRight = true;
            }
            else if (gesture == KinectGestures.Gestures.Up)
            {
                swipeUp = true;
            }
            else if (gesture == KinectGestures.Gestures.Down)
            {
                swipeDown = true;
            }
            else if (gesture== KinectGestures.Gestures.turnPageRightByLeftHand)
            {
                turnPageRight = true;
            }
            else if (gesture == KinectGestures.Gestures.turnPageLeftByLeftHand)
            {
                turnPageLeft = true;
            }
        }
        return true;
    }

    public virtual void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userId != manager.GetPrimaryUserID()))
            return;

        if (BaseObjectMovement.actionState == BaseObjectMovement.ActionState.Wait)
        {

            if (gesture == KinectGestures.Gestures.Left)
            {
                if (gestureInfo != null)
                {
                    gestureInfo.GetComponent<GUIText>().text = "可向左！";
                }
            }
            else if (gesture == KinectGestures.Gestures.Right)
            {
                if (gestureInfo != null)
                {
                    gestureInfo.GetComponent<GUIText>().text = "可向右！";
                }
            }
            else if (gesture == KinectGestures.Gestures.Up)
            {
                if (gestureInfo != null)
                {
                    gestureInfo.GetComponent<GUIText>().text = "可上升！";
                }
            }
            else if (gesture == KinectGestures.Gestures.Down)
            {
                if (gestureInfo != null)
                {
                    gestureInfo.GetComponent<GUIText>().text = "可下降！";
                }
            }
        }
        else if (BaseObjectMovement.actionState == BaseObjectMovement.ActionState.Delay)
        {
            if (gestureInfo != null && BaseObjectMovement.delayTime >= 0)
            {
                gestureInfo.GetComponent<GUIText>().text = BaseObjectMovement.delayTime.ToString();
            }
            BaseObjectMovement.delayTime -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 添加追踪识别的手势
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userIndex"></param>
    public virtual void UserDetected(long userId, int userIndex)
    {
        KinectManager manager = KinectManager.Instance;
        
        manager.DetectGesture(userId, KinectGestures.Gestures.Up);
        manager.DetectGesture(userId, KinectGestures.Gestures.Down);
        manager.DetectGesture(userId, KinectGestures.Gestures.Left);
        manager.DetectGesture(userId, KinectGestures.Gestures.Right);

        manager.DetectGesture(userId, KinectGestures.Gestures.turnPageRightByLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.turnPageLeftByLeftHand);
        
        #region 注释

        //manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        //manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        //manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        //manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);

        //manager.DetectGesture(userId, KinectGestures.Gestures.turn_Up);
        //manager.DetectGesture(userId, KinectGestures.Gestures.turn_Down);
        //manager.DetectGesture(userId, KinectGestures.Gestures.turn_Left);
        //manager.DetectGesture(userId, KinectGestures.Gestures.turn_Right);

        //if (gestureInfo != null)
        //{
        //    gestureInfo.GetComponent<GUIText>().text = "发现手势！ "; //+ manager.get
        //}
        #endregion
    }

    public virtual void UserLost(long userId, int userIndex)
    {
        KinectManager manager = KinectManager.Instance;
        if (!manager || (userId != manager.GetPrimaryUserID()))
            return;
        //if (gestureInfo != null)
        //{
        //    gestureInfo.GetComponent<GUIText>().text = "手势消失了！";
        //}
    }

}
