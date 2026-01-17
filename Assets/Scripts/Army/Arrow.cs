using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 25f;
    private Rigidbody2D rb;
    private Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection(rb.linearVelocity);
    }
    void FixedUpdate()
    {
        if(!target)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == target)
        {
            collision.GetComponent<Hp>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    void changeDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
