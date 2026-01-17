using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArcherTower : MonoBehaviour,IPointerDownHandler,  IPointerExitHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject ui;
    [SerializeField] private Button upBTN;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        upBTN.onClick.AddListener(()=>UpgradeTower());
    }
    public void OpenUpgradeUI()
    {
        ui.SetActive(true);
    }
    public void CloseUpgradeUI()
    {
        ui.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OpenUpgradeUI();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        CloseUpgradeUI();
    }
    
    void UpgradeTower()
    {
        animator.SetTrigger("upgrade");
    }
}