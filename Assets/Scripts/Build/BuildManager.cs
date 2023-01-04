using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] private NodeUI nodeUI;

    private TowerBlueprint objectToBuild;
    private Node selectedNode;

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

        SoundManager.Instance.PlayDeploy();

        node.tower = tower;
        node.tower.GetComponent<Tower>().Addprice(objectToBuild.cost);
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        objectToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SetTowerToBuild(TowerBlueprint tower)
    {
        objectToBuild = tower;

        DeselectNode();
    }
}
