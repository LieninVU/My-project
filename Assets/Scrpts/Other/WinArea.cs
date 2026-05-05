using UnityEngine;
using System;

public class WinArea : MonoBehaviour
{
    public event Action OnShowWinMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            OnShowWinMenu?.Invoke();
        }
    }
}
