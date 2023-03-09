using System;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
        OnMoneyChanged(0);
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _text.text = money.ToString();
    }
}
