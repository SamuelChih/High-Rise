using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform swipePoint;
    [SerializeField] private GameObject[] swipes;


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
        if(Input.GetKeyDown(KeyCode.L) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
        
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        swipes[0].transform.position = swipePoint.position;
        swipes[0].GetComponent<SwipeOfAttack>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    
}
