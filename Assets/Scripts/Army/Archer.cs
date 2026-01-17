using UnityEngine;

public class Archer : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowsPerSecond = 1f;
    [SerializeField] private float buffer = 1.2f;
    private float timeUntilNextArrow;
    private Transform target;
    private float dirX;
    private float dirY;
    private Animator animator;
    private bool isFacingHorizontal=true;
    private Vector2 direction;
    private bool isShooting=false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target == null)
        {
            FindTarget();
            return;
        }
        direction = target.position - transform.position;
        RotateForwardTarget(direction);
        UpdateDirection(direction);
        animator.SetFloat("dirX", dirX);
        animator.SetFloat("dirY", dirY);
        Flip();
        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntilNextArrow += Time.deltaTime;
            if(timeUntilNextArrow >= (1f/ arrowsPerSecond))
            {
                Shoot();
                timeUntilNextArrow = 0f;
            }
        }
        
    }
    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetTarget(target);
        animator.SetTrigger("shoot");
        // timeUntilNextArrow = 0f;
        
    }
    void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange,(Vector2)transform.position,0f,enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    void RotateForwardTarget(Vector2 direction)
    {
        // IMPORTANT: decide axis based on RELATIVE direction, not target world position.
        // Using target.position causes unwanted flips (e.g., left side but y is large => switches to vertical => dirX becomes 0).
        if (isFacingHorizontal)
        {
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) * buffer)
            {
                isFacingHorizontal = false;
            }
        }
        else
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) * buffer)
            {
                isFacingHorizontal = true;
            }
        }
    }
    void UpdateDirection(Vector2 direction)
    {
        if (isFacingHorizontal)
        {
            dirX = direction.x > 0 ? 1 : -1;
            dirY = 0;
        }
        else
        {
            dirY = direction.y > 0 ? 1 : -1;
            dirX = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void Flip()
    {
        if(dirX >= 0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if(dirX <= -0.01f)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }
    bool CheckTargetInRange()
    {
        return Vector2.Distance(transform.position, target.position) <= attackRange;
    }
}
