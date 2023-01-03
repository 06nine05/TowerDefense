using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject hover;

    public GameObject tower;

    private BuildManager buildManager;
    
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    private void OnMouseDown()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (tower != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        buildManager.BuildTower(this);
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }

        hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
        hover.SetActive(false);
    }
}
