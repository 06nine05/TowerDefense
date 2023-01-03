using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [SerializeField] private TowerBlueprint towerA;
    [SerializeField] private TowerBlueprint towerB;
    [SerializeField] private TowerBlueprint towerC;

    private BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectTowerA()
    {
        Debug.Log("Type A Selected");
        buildManager.SetTowerToBuild(towerA);
    }

    public void SelectTowerB()
    {
        Debug.Log("Type B Selected");
        buildManager.SetTowerToBuild(towerB);
    }

    public void SelectTowerC()
    {
        Debug.Log("Type C Selected");
        buildManager.SetTowerToBuild(towerC);
    }
}