using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0, Space.World);

        
        lifetime += Time.deltaTime;
        if (lifetime > 5f) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (hit) return;

        hit = true;

        
        if (anim != null)
        {
            anim.SetTrigger("explode");
        }

        
        Invoke("Deactivate", 0.3f);
    }

    public void SetDirection(float _direction)
    {

        CancelInvoke();

        lifetime = 0;
        direction = _direction;
        hit = false;
        gameObject.SetActive(true);
        boxCollider.enabled = true;

        
        float localScaleX = Mathf.Abs(transform.localScale.x) * _direction;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

