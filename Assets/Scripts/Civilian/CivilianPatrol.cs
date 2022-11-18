using UnityEngine;

public class CivilianPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Civilian")]
    [SerializeField] private Transform civilian;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Civilian Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = civilian.localScale;
    }

    private void Update()
    {
        
            if(movingLeft)
            {
                if(civilian.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if (civilian.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }      
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("moving", true);

        //make civilian face direction (KEEP THE "0-initScale" OR DIRECTION WILL BE INVERTED)
        civilian.localScale = new Vector3(0 - initScale.x * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        civilian.position = new Vector3(civilian.position.x + Time.deltaTime * _direction * speed,
            civilian.position.y, civilian.position.z);

    }
}
