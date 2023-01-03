using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int startMoney;
    [SerializeField] private int startLife;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI lifeText;

    public static int Money;
    public static int life;

    private void Start()
    {
        Money = startMoney;
        life = startLife;
    }

    private void Update()
    {
        moneyText.text = $"Money : {Money}";
        lifeText.text = $"Life : {life}";
    }
}
