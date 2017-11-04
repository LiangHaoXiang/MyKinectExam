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

public class ThirdPersonCamera : MonoBehaviour
{
	[Tooltip("Smoothing factor of camera motion.")]
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion

	private Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	private Transform lookAtPos;			// the position to move the camera to when using head look


	void Start()
	{
		// initialising references
		standardPos = GameObject.Find ("CamPos").transform;
		
		if(GameObject.Find ("LookAtPos"))
			lookAtPos = GameObject.Find ("LookAtPos").transform;
	}
	
	void FixedUpdate ()
	{
		// if we hold Alt
		if(Input.GetButton("Fire2") && lookAtPos)
		{
			// lerp the camera position to the look at position, and lerp its forward direction to match 
			transform.position = Vector3.Lerp(transform.position, lookAtPos.position, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
		}
		else
		{	
			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		}
		
	}
}
