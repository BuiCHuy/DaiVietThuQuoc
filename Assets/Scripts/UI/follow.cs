using UnityEngine;

public class follow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position;
    }
}
