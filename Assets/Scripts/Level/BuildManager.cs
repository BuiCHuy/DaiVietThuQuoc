using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    [SerializeField] private GameObject[] towerPrefabs;
    public int selectedTowerIndex = 0;

    void Awake()
    {
        main = this;
    }
    public GameObject GetTowerToBuild()
    {
        return towerPrefabs[selectedTowerIndex];
    }
}
