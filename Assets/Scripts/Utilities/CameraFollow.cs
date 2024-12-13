using UnityEngine;

//This script is to make the Camera follow the player
namespace Utilities
{
    public class CameraFollow : MonoBehaviour

    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 locationOffset;
        public Vector3 rotationOffset;
        

        void LateUpdate()
        {
            
            //var rotation = target.rotation;
            Vector3 desiredPosition = target.position + locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        /*
            Quaternion desiredrotation = rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
          */ 
            
        }
    }
}