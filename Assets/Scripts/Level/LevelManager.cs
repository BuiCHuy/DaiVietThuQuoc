using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    [SerializeField] public Transform start;
    [SerializeField] public Transform[] waypoints;
    private void Awake()
    {
        main = this;
    }
}
