using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    private Transform target;
    private int waypointIndex = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = LevelManager.main.waypoints[0];
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
            if(waypointIndex >= LevelManager.main.waypoints.Length)
            {
                Spawn.onEnemyDestroyed.Invoke();
                Destroy(gameObject);
                return;
            }
            else target = LevelManager.main.waypoints[waypointIndex];
        }
        Flip();
        animator.SetFloat("xVel", rb.linearVelocity.x);
        animator.SetFloat("yVel", rb.linearVelocity.y);
    }
    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; 
        rb.linearVelocity = direction * speed;
    }
    void Flip()
    {
        if(rb.linearVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if(rb.linearVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }
    
}
