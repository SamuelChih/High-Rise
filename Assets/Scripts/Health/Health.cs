using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float healingSpeed;
    public float currentHealth { get; private set; }

    private Animator anim;
    private bool isHealing;
    private bool dead;

    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        isHealing = false;
    }

    public void TakeDamage(float _damage)
    {
        // Ensures we never go below 0 health or have more health than the start
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);

        if (currentHealth > 0)
        {
            // player hurt
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Player
                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;


                //Civilian
                if(GetComponentInParent<CivilianPatrol>() != null)
                {   
                    GetComponentInParent<CivilianPatrol>().enabled = false;
                    Destroy(gameObject.GetComponent<BoxCollider2D>());    
                }

                //Cop
                if(GetComponentInParent<CopEnemyPatrol>() != null)
                {
                    GetComponentInParent<CopEnemyPatrol>().enabled = false;
                    Destroy(gameObject.GetComponent<BoxCollider2D>());
                }

                if(GetComponent<CopEnemy>() != null)
                {
                    GetComponent<CopEnemy>().enabled = false;
                }

                dead = true;
            }
        }
    }

    public void StartHealing()
    {
        if (!isHealing && currentHealth < startingHealth)
        {
            isHealing = true;
            float end = currentHealth + 1.0f;
            StartCoroutine(Heal(end));
        }
    }

    IEnumerator Heal(float end)
    {
        if(GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = false;
        if(GetComponent<PlayerAttack>() != null)
            GetComponent<PlayerAttack>().enabled = false;
        while (currentHealth < end)
        {
            currentHealth = Mathf.Clamp(currentHealth + healingSpeed, 0 , end);
            yield return new WaitForSeconds(0.1f);
        }
        if(GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;
        if(GetComponent<PlayerAttack>() != null)
            GetComponent<PlayerAttack>().enabled = true;
        isHealing = false;
    }
}
