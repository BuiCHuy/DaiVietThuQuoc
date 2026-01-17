using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Plot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button archerBtn;
    [SerializeField] private Button towerBtn;
    [SerializeField] private Button upgradeBtn;
    private GameObject tower;
    private Color startColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //upgradeBtn = tower.GetComponent<ArcherTower>().upBTN;
        startColor = sr.color;
        archerBtn.onClick.AddListener(() => BuildTower(0));
        towerBtn.onClick.AddListener(() => BuildTower(1));
        //upgradeBtn.onClick.AddListener(UpgradeTower);
    }
    void UpgradeTower()
    {
        
            tower.GetComponent<ArcherTower>().OpenUpgradeUI();
        
        ;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = hoverColor;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = startColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Plot clicked");
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(tower != null)
            {
                //upgradeUI.SetActive(true);
                return;
            }
            else
            {
                buildUI.SetActive(true);
            }
        }
    }
    void Update()
    {
        if(tower != null)
        {
            archerBtn.interactable = false;
            towerBtn.interactable = false;
        }
        else
        {
            archerBtn.interactable = true;
            towerBtn.interactable = true;
        }
        
    }
    void OnDestroy()
    {
        archerBtn.onClick.RemoveAllListeners();
        towerBtn.onClick.RemoveAllListeners();
    }
    void BuildTower(int index)
    {
        BuildManager.main.selectedTowerIndex = index;
        GameObject towerBuild = BuildManager.main.GetTowerToBuild();
        tower = Instantiate(towerBuild, transform.position, Quaternion.identity);
        buildUI.SetActive(false);
    }

}
