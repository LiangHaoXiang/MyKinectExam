/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class SimpleVisualGestureListener : MonoBehaviour, VisualGestureListenerInterface
{
	[Tooltip("GUI-Text to display the discrete gesture information.")]
	public GUIText discreteInfo;

	[Tooltip("GUI-Text to display the continuous gesture information.")]
	public GUIText continuousInfo;


	private bool discreteGestureDisplayed;
	private bool continuousGestureDisplayed;

	private float discreteGestureTime;
	private float continuousGestureTime;


	public void GestureInProgress(long userId, int userIndex, string gesture, float progress)
	{
		if(continuousInfo != null)
		{
			string sGestureText = string.Format ("{0} {1:F0}%", gesture, progress * 100f);
			continuousInfo.GetComponent<GUIText>().text = sGestureText;

			continuousGestureDisplayed = true;
			continuousGestureTime = Time.realtimeSinceStartup;
		}
	}

	public bool GestureCompleted(long userId, int userIndex, string gesture, float confidence)
	{
		if(discreteInfo != null)
		{
			string sGestureText = string.Format ("{0}-gesture detected, confidence: {1:F0}%", gesture, confidence * 100f);
			discreteInfo.GetComponent<GUIText>().text = sGestureText;

			discreteGestureDisplayed = true;
			discreteGestureTime = Time.realtimeSinceStartup;
		}

		// reset the gesture
		return true;
	}
	
	public void Update()
	{
		// clear gesture infos after a while
		if(continuousGestureDisplayed && ((Time.realtimeSinceStartup - continuousGestureTime) > 1f))
		{
			continuousGestureDisplayed = false;

			if(continuousInfo != null)
			{
				continuousInfo.GetComponent<GUIText>().text = string.Empty;
			}
		}

		if(discreteGestureDisplayed && ((Time.realtimeSinceStartup - discreteGestureTime) > 1f))
		{
			discreteGestureDisplayed = false;
			
			if(discreteInfo != null)
			{
				discreteInfo.GetComponent<GUIText>().text = string.Empty;
			}
		}
	}

}
