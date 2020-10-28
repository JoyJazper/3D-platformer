using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 10f, -8f);
    private Transform playerTransform;
    private Transform cameraRig;
    private Vector3 rigPosition;
    public Transform PlayerTransform{
        set{
            playerTransform = value;
        }
    }

    private void Start() {
        SetCamera();
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void SetCamera(){
        cameraRig = transform.parent;
        transform.position = rigPosition + offset;
    }
    private void FollowPlayer(){
        rigPosition = playerTransform.transform.position;
        cameraRig.transform.position = rigPosition;
        cameraRig.transform.rotation = playerTransform.transform.rotation;
    }
}