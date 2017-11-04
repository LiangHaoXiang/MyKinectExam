using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGestureListener2 : MonoBehaviour,KinectGestures.GestureListenerInterface
{
    public Text gestureInfo;
    private bool state = false;//识别状态，0表示未识别，1表示已识别
    public float intervalTime = 5.0f;
    private bool intervalBegin = false;
    private int quiet = 0;
    private float quiettime = 5.0f;
    public enum 手势
    {
        剪刀,
        石头,
        布
    }
    private 手势 RandomShoushi()
    {
        System.Random ran = new System.Random();
        int randomnum = ran.Next(0, 2);
        return (手势)randomnum;
    }

    private string getInfo(KinectInterop.HandState show, 手势 shoushi)
    {
        string info = string.Empty;
        switch (show)
        {
            case KinectInterop.HandState.Closed:
                info = "你出的是石头\n";
                break;
            case KinectInterop.HandState.Lasso:
                info = "你出的是剪刀\n";
                break;
            case KinectInterop.HandState.Open:
                info = "你出的是布\n";
                break;
            default:
                info = "请出招...\n";
                return info;
        }

        switch (shoushi)
        {
            case 手势.石头:
                info += "电脑出的是石头\n";
                break;
            case 手势.剪刀:
                info += "电脑出的是剪刀\n";
                break;
            case 手势.布:
                info += "电脑出的是布\n";
                break;
        }
        int res = contrast(show, shoushi);
        if (res == 1)
        {
            info += "哈哈哈，你赢了\n";
        }
        else if (res == -1)
        {
            info += "哈哈哈，你输了\n";
        }
        else if (res == 0)
        {
            info += "哈哈哈，平手";
        }
        else
        {
            info += "你的手势未识别";
        }

        state = true;//识别完成
        return info;

    }


    private int contrast(KinectInterop.HandState show, 手势 shoushi)
    {
        int rssult = 0;

        switch (show)
        {
            case KinectInterop.HandState.Closed:
                switch (shoushi)
                {
                    case 手势.石头:
                        rssult = 0;
                        break;
                    case 手势.剪刀:
                        rssult = 1;
                        break;
                    case 手势.布:
                        rssult = -1;
                        break;
                }
                break;
            case KinectInterop.HandState.Lasso:
                switch (shoushi)
                {
                    case 手势.石头:
                        rssult = -1;
                        break;
                    case 手势.剪刀:
                        rssult = 0;
                        break;
                    case 手势.布:
                        rssult = 1;
                        break;
                }
                break;
            case KinectInterop.HandState.Open:
                switch (shoushi)
                {
                    case 手势.石头:
                        rssult = 1;
                        break;
                    case 手势.剪刀:
                        rssult = -1;
                        break;
                    case 手势.布:
                        rssult = 0;
                        break;
                }
                break;
            default:
                rssult = 10;
                break;
        }
        return rssult;
    }
    void Update()
    {

        if (intervalBegin)
        {
            if (intervalTime > 0)
            {
                intervalTime -= Time.deltaTime;
            }
            else
            {
                intervalBegin = false;
                intervalTime = 5.0f;
                state = false;
            }
        }

        if (!state)
        {
            KinectManager _manager = KinectManager.Instance;
            long userid = _manager.GetUserIdByIndex(0);
            gestureInfo.text = getInfo(_manager.GetRightHandState(userid), RandomShoushi());
        }
        if (quiet == 1)
        {
            gestureInfo.text = "再右挥一次就不跟你玩了...";
            if (quiet == 2)
            {
                Debug.Log("退出");
                Application.Quit();

            }
            if (quiettime < 0)
            {
                quiet = 0;
                quiettime = 5.0f;
                gestureInfo.text = "请出招...";
            }
            else
            {
                quiettime -= Time.deltaTime;
            }
        }
    }

    public void UserDetected(long userId, int userIndex)
    {
        //throw new NotImplementedException();
        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
    }

    public void UserLost(long userId, int userIndex)
    {
        //throw new NotImplementedException();
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //throw new NotImplementedException();
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //throw new NotImplementedException();
        if (gesture == KinectGestures.Gestures.SwipeLeft)
        {
            intervalBegin = true;
            gestureInfo.text = "准备出招...";
        }
        if (gesture == KinectGestures.Gestures.SwipeRight)
        {
            quiet++;
        }
        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        //throw new NotImplementedException();
        return true;
    }
}
