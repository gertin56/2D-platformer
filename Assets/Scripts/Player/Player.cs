using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _money;

    public int Money => _money;

    public UnityAction<int> MoneyChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin = collision.GetComponent<Coin>();

        if (coin != null)
        {
            TakeCoin(coin.Reward);
            collision.gameObject.SetActive(false);
        }
    }

    private void TakeCoin(int reward)
    {
        _money += reward;
        MoneyChanged.Invoke(Money);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
