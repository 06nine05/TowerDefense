using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public GameObject prefabA;
    public GameObject prefabB;
    public GameObject prefabC;

    private TowerBlueprint objectToBuild;

    public bool CanBuild { get { return objectToBuild != null; } }

    public void BuildTower(Node node)
    {
        if (PlayerStat.Money < objectToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStat.Money -= objectToBuild.cost;

        GameObject tower = Instantiate(objectToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);
        node.tower = tower;
    }

    public void SetTowerToBuild(TowerBlueprint tower)
    {
        objectToBuild = tower;
    }
}
