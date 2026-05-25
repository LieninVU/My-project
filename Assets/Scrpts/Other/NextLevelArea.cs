using System;
using UnityEngine;

public class NextLevelArea : MonoBehaviour
{
    public event Action OnShowNextLevelMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            OnShowNextLevelMenu?.Invoke();
        }
    }
}
