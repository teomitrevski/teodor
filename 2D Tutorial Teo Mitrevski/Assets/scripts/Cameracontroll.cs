using Unity.VisualScripting;
using UnityEngine;

public class Cameracontroll : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime = 0.3f;

    private float currentPosX;
    private Vector3 linearVelocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref linearVelocity, smoothTime);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

}
