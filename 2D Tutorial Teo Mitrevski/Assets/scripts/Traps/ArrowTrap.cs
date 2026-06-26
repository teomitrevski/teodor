using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;
    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = FirePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}