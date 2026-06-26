using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        playerHealth.Respawn(); //Restore player health and reset animation

        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position; //Move player to checkpoint location

            //Move the camera to the checkpoint's room
            if (Camera.main != null && Camera.main.GetComponent<Cameracontroll>() != null)
            {
                Camera.main.GetComponent<Cameracontroll>().MoveToNewRoom(currentCheckpoint.parent);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;

            if (SoundManager.instance != null && checkpoint != null)
            {
                SoundManager.instance.PlaySound(checkpoint);
            }

            collision.GetComponent<Collider2D>().enabled = false;

            if (collision.GetComponent<Animator>() != null)
            {
                collision.GetComponent<Animator>().SetTrigger("appear");
            }
        }
    }
}