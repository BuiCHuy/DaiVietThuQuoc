using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    private EnemyHp enemyHp;    

    void Start()
    {
        currentHp = maxHp;
        enemyHp = GetComponentInChildren<EnemyHp>();
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        enemyHp.UpdateHpBar(currentHp, maxHp);
        if(currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
