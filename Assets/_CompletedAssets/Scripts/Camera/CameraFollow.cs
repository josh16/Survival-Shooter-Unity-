using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
		public Transform target;
		public float smoothing = 5f;

		Vector3 offset;


		void Start()
		{
			/*Subtracting the offset position of the camera away from the target position*/
			offset = transform.position - target.position; 

		}

       
		void FixedUpdate()
		{
			/*Finding a place for the camera to be, which is above the level*/
			Vector3 targetCamPos = target.position + offset; 
			/*The Lerp is smoothing between the current position towards the new position and being smoothed out*/
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}

		//Note to self, turn of hasexit in the animator
	}
}