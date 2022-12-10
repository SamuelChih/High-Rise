using UnityEngine;

public class SwipeOfAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private bool isHeal;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private Health playerHealth;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() 
    {
        if (hit && isHeal)
        {
            playerHealth.StartHealing();
            hit = false;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime >= 0.3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            hit = true;
            boxCollider.enabled = false;
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void SetHeal(bool temp)
    {
        isHeal = temp;
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }

}
