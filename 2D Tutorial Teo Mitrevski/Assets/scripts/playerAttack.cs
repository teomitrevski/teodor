using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private float Cooldowntimer = Mathf.Infinity;
    private Animator anim;
    private playermovement playerMovementScript;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovementScript = GetComponent<playermovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Cooldowntimer > attackCooldown && playerMovementScript.canAttack())
            Attack();

        Cooldowntimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        Cooldowntimer = 0;
    }

    private void LaunchFireball()
    {
        int fireballIndex = FindFireball();

        if (fireballIndex == -1) return;

        fireballs[fireballIndex].transform.position = firePoint.position;
        fireballs[fireballIndex].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        fireballs[fireballIndex].SetActive(true);
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (fireballs[i] != null && !fireballs[i].activeInHierarchy)
                return i;
        }

        return -1;
    }



}