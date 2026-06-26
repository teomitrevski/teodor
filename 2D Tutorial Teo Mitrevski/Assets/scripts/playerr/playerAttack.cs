using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip FireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement != null && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (SoundManager.instance != null && FireballSound != null)
        {
            SoundManager.instance.PlaySound(FireballSound);
        }

        anim.SetTrigger("attack");
        cooldownTimer = 0;

        int i = FindFireball();

        if (fireballs[i] != null)
        {
            fireballs[i].transform.position = firePoint.position;
            fireballs[i].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            fireballs[i].SetActive(true);
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (fireballs[i] != null && !fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}