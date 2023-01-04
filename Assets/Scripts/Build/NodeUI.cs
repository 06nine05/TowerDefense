using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiPanel;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI upgradeText;

    private Node target;
    private Tower towerstat;
    private int cost;

    public void Upgrade()
    {
        if (PlayerStat.Money < cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStat.Money -= cost;

        SoundManager.Instance.PlaySell();

        towerstat.LevelUp();
        towerstat.Addprice(cost);

        ShowStat();
    }

    public void Sell()
    {
        PlayerStat.Money += towerstat.GetCost() / 2;

        SoundManager.Instance.PlaySell();

        Destroy(target.tower);

        Hide();
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        towerstat = target.tower.GetComponent<Tower>();

        ShowStat();

        Time.timeScale = 0.2f;

        uiPanel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;

        uiPanel.gameObject.SetActive(false);
    }

    private void UpgradeCost()
    {
        if (towerstat.GetLvl() == 5)
        {
            cost = 100;
        }

        cost = 20 * towerstat.GetLvl();
    }

    private void ShowStat()
    {
        UpgradeCost();

        switch (towerstat.GetTowerType())
        {
            case Tower.TowerType.A_Turret:
                nameText.text = "Turret";
                descriptionText.text = "Attack nearest enemy with fast attack speed";
                break;
            case Tower.TowerType.B_Cannon:
                nameText.text = "Cannon";
                descriptionText.text = $"Attack furthest enemy and Deal splash damage equal to {towerstat.GetAOEMod()}% of Tower's attack to surrounding enemies";
                break;
            case Tower.TowerType.C_Slow:
                nameText.text = "Slow Turret";
                descriptionText.text = $"Attack nearest enemy and slow enemy by {towerstat.GetSlowEffect()} for {towerstat.GetSlowDuration()} sec";
                break;
        }

        lvlText.text = $"Level : {towerstat.GetLvl()}";
        atkText.text = $"Attack : {towerstat.GetAtk()}";
        speedText.text = $"Attack Delay : {towerstat.GetAtkSpd()}sec";
        rangeText.text = $"Range : {towerstat.GetRange() - 0.5f}m";
        sellText.text = $"{towerstat.GetCost() / 2} $";
        upgradeText.text = $"{cost} $";

        if (towerstat.GetLvl() == 5)
        {
            upgradeButton.interactable = false;
        }
    }
}
