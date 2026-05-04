using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private void Start()
    {
        Player.Instance.OnHealthChange += PlayerOnHealthChange;
        //PlayerOnHealthChange(Player.Instance.GetCurrentHealth(), Player.Instance.GetMaxHealth());
    }

    private void PlayerOnHealthChange(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void OnDestroy()
    {
        Player.Instance.OnHealthChange -= PlayerOnHealthChange;
    }
}
