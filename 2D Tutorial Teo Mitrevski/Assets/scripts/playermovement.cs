using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumppower;
    [SerializeField] private float walljumpforce;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider2d;
    private float walljumpcooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        walljumpcooldown = 0.3f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (walljumpcooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        
        walljumpcooldown += Time.deltaTime;

        if (onwall() && !isgrounded())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isgrounded());
    }

    private void jump()
    {
        if (isgrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumppower);
            anim.SetTrigger("jump");
        }
        else if (onwall())
        {
            walljumpcooldown = 0;

            float facingDirection = Mathf.Sign(transform.localScale.x);

            body.linearVelocity = new Vector2(-facingDirection * walljumpforce, jumppower * 0.8f);
            transform.localScale = new Vector3(-facingDirection, 1, 1);

            anim.SetTrigger("jump");
        }
    }

    private bool isgrounded()
    {
        Bounds bounds = boxCollider2d.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(bounds.center.x, bounds.min.y), new Vector2(bounds.size.x * 0.9f, 0.1f), 0f, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    private bool onwall()
    {
        Bounds bounds = boxCollider2d.bounds;
        float facingDirection = Mathf.Sign(transform.localScale.x);
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(facingDirection == 1 ? bounds.max.x : bounds.min.x, bounds.center.y), new Vector2(0.1f, bounds.size.y * 0.9f), 0f, new Vector2(facingDirection, 0), 0.1f, walllayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isgrounded() && !onwall();
    }

}














