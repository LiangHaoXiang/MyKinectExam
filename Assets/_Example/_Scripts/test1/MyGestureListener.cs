using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGestureListener : MonoBehaviour,KinectGestures.GestureListenerInterface {
    [Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
    public GUIText gestureInfo;

    // private bool to track if progress message has been displayed
    private bool progressDisplayed;
    private float progressGestureTime;

    //当识别到用户时调用该函数
    public void UserDetected(long userId, int userIndex)
    {
        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        //manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        //manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanLeft);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeanRight);
        //manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        //manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);

        manager.DetectGesture(userId, KinectGestures.Gestures.Run);

        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<GUIText>().text = "Swipe, Jump, Squat or Lean.";
        }
        Debug.Log("发现用户");
    }
    
    //当失去用户时出发
    public void UserLost(long userId, int userIndex)
    {
        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<GUIText>().text = string.Empty;
        }
        Debug.Log("失去用户");
    }

    /// <summary>
    /// Invoked when a gesture is in progress.
    /// </summary>
    /// <param name="userId">被识别者的id</param>
    /// <param name="userIndex">被识别者的序号</param>
    /// <param name="gesture">手势类型</param>
    /// <param name="progress">手势识别的进度，可以认为是相似度。范围是[0,1]</param>
    /// <param name="joint">关节类型</param>
    /// <param name="screenPos">视图坐标的单位向量</param>
    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //主要将一些需要动态监测的手势放在这个函数下
        //比如说缩放、滚轮都是依据你两手之间的距离来判断应该缩放或旋转多少度
        
        //监测缩放，如果相似度大于50%
        if ((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - {1:F0}%", gesture, screenPos.z * 100f);
                gestureInfo.GetComponent<GUIText>().text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
        else if ((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft ||
                 gesture == KinectGestures.Gestures.LeanRight) && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - {1:F0} degrees", gesture, screenPos.z);
                gestureInfo.GetComponent<GUIText>().text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
        else if (gesture == KinectGestures.Gestures.Run && progress > 0.5f)
        {
            if (gestureInfo != null)
            {
                string sGestureText = string.Format("{0} - progress: {1:F0}%", gesture, progress * 100);
                gestureInfo.GetComponent<GUIText>().text = sGestureText;

                progressDisplayed = true;
                progressGestureTime = Time.realtimeSinceStartup;
            }
        }
    }

    /// <summary>
    /// 当一个手势识别完成后被调用
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">被识别者的ID</param>
    /// <param name="userIndex">被识别者的序号</param>
    /// <param name="gesture">被识别到的手势类型</param>
    /// <param name="joint">被识别到的关节类型</param>
    /// <param name="screenPos">视图坐标的单位向量</param>
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        if (progressDisplayed)
            return true;

        string sGestureText = gesture + " detected";
        if (gestureInfo != null)
        {
            gestureInfo.GetComponent<GUIText>().text = sGestureText;
        }

        return true;
    }



    //参数同上，在手势被取消的时候调用
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        if (progressDisplayed)
        {
            progressDisplayed = false;

            if (gestureInfo != null)
            {
                gestureInfo.GetComponent<GUIText>().text = String.Empty;
            }
        }

        return true;
    }

    public void Update()
    {
        if (progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
        {
            progressDisplayed = false;

            if (gestureInfo != null)
            {
                gestureInfo.GetComponent<GUIText>().text = String.Empty;
            }

            Debug.Log("Forced progress to end.");
        }
    }


}
