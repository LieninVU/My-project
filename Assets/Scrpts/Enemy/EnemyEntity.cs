using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currenHealth;

    private void Start()
    {
        currenHealth = maxHealth;
    }

    public void TakeHealth(int damage)
    {
        currenHealth -= damage;

        DetectedDeath();
    }

    private void DetectedDeath()
    {
        if(currenHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
