using JetBrains.Annotations;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform Previusroom;
    [SerializeField] private Transform Nextroom;
    [SerializeField] private Cameracontroll cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(Nextroom);
            }
            else
            {
                cam.MoveToNewRoom(Previusroom);
            }
        }

    }
}
