using UnityEngine;
using UnityEngine.UI;
public class EnemyHp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider slider;
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        slider.value = currentHp / maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
