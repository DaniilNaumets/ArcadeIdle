using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private MoneyCounter _moneyCounter;
    [SerializeField] private GuestInstaniate _guestInstaniate;
    [SerializeField] private UIForPlayer _ui;
    [SerializeField] private CashMachine _cashMachine;
    [SerializeField] private AIEmployee _employee;
    [SerializeField] private TakeMoneyTrigger _takeMoneyTrigger;

    [SerializeField] private Button[] _upgradeButtons;

    [SerializeField] private bool[] upgrades;

    public bool[] Upgrades
    {
        get { return upgrades; }
        set { upgrades = value; }
    }

    // 1 ���������

    [SerializeField] private GameObject _plate;
    [SerializeField] private GameObject _saler;

    // 5 ���������

    [SerializeField] private GameObject _plateTwo;
    [SerializeField] private GameObject _cashier;

    // 6 ���������

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TextMeshProUGUI _endText;


    /// <summary>
    /// 1 ��������� - ���������� ��������
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void AddSaler(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 0);
            _plate.SetActive(false);
            _saler.SetActive(true);
        }

    }


    /// <summary>
    /// 2 ��������� - ���������� �������� ��������� �������� � 2 ����
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void NewSpawnIntervalForGuests(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 1);
            _guestInstaniate.ChangeSpawnInterval(_guestInstaniate.SpawnInterval / 2);
        }
    }


    /// <summary>
    /// 3 ��������� - ������������� ���� �� �������
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void DoublePrice(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 2);
            _cashMachine.CashForOnePiece *= 2;
        }
    }

    /// <summary>
    /// 4 ��������� - ���������� �������� ������� � 5 ���
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void DoublePrepare(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 3);
            _employee.TimeForPrepare /= 5;
        }
    }

    /// <summary>
    /// 5 ��������� - ���������� �������
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void AddCashier(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 4);
            _plateTwo.SetActive(false);
            _cashier.SetActive(true);
            _takeMoneyTrigger.IsCashierHere = true;
        }
    }


    /// <summary>
    /// 6 ��������� - ����� ����
    /// </summary>
    /// <param name="price">��������� ���������</param>
    public void EndGameUpgrade(int price)
    {
        if (_moneyCounter.Coins >= price)
        {
            Upgrade(price, 5);
            _endPanel.SetActive(true);
            _endText.text = $"�����: {Convert.ToInt32(Time.timeSinceLevelLoad)} ������";
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void Upgrade(int price, int num)
    {
        _moneyCounter.Coins -= price;
        _upgradeButtons[num].interactable = false;
        upgrades[num] = true;
        _ui.RefreshMoneyText(_moneyCounter.Coins);
    }

    public bool[] GetCurrentUpgrades()
    {
        return upgrades;
    }

    public void SetCurrentUpgrades(bool[] upgr)
    {
        upgrades = upgr;
    }

    public void CheckUpgrades()
    {

        for (int i = 0; i < upgrades.Length; i++)
        {
            if (upgrades[i])
            switch (i)
            {
                case 0: AddSaler(0); break;
                case 1: NewSpawnIntervalForGuests(0); break;
                case 2: DoublePrice(0); break;
                case 3: DoublePrepare(0); break;
                case 4: AddCashier(0); break;
            }
        }
    }
}
