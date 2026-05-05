using System;
using UnityEngine;

public class Elixir : MonoBehaviour
{
    [SerializeField] private int treathmentAmount = 5;
    public event Action OnDestructibleElixir;
    public event Action OnGetTreathment;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Player.Instance.Heal(treathmentAmount);
            OnDestructibleElixir?.Invoke();
            Destroy(gameObject);
        }
    }
}
