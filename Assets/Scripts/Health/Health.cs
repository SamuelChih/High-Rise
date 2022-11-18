using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake() 
    {
        
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        
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
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    private void Update() {
       if (Input.GetKeyDown(KeyCode.E))
       TakeDamage(1); 
    }

    
}
