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
        if (collision.GetComponent<Coin>())
        {
            TakeCoin();
            collision.gameObject.SetActive(false);
        }
    }

    private void TakeCoin()
    {
        _money += 1;
        MoneyChanged.Invoke(Money);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
