using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform swipePoint;
    [SerializeField] private GameObject[] swipes;


    private Animator anim;
    private PlayerMovement playerMovement;
    private Health playerHealth;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && cooldownTimer > attackCooldown) // && playerMovement.canAttack())
        {
            NormalAttack();
        }
        if(Input.GetKeyDown(KeyCode.K) && cooldownTimer > attackCooldown) // && playerMovement.canAttack())
        {
            HealAttack();
        }

        cooldownTimer += Time.deltaTime;
        
    }

    private void Attack(bool isHeal)
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        swipes[0].transform.position = swipePoint.position;
        swipes[0].GetComponent<SwipeOfAttack>().SetHeal(isHeal);
        swipes[0].GetComponent<SwipeOfAttack>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private void NormalAttack()
    {
        Attack(false);
    }

    private void HealAttack()
    {
        Attack(true);
    }
    
}
