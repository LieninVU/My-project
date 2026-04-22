using System;
using UnityEngine;
public class Sword : MonoBehaviour
{
    [SerializeField] private int DAMAGE = 5;
    public event EventHandler onSwordSwing;
    private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackColliderOff();
    }

    public void Attack()
    {
        AttackColliderOff_and_On();
        onSwordSwing.Invoke(this, EventArgs.Empty);
    }

    public void AttackColliderOff()
    {
        polygonCollider2D.enabled = false;
    }
    private void AttackColliderOn()
    {
        polygonCollider2D.enabled = true;
    }

    private void AttackColliderOff_and_On()
    {
        AttackColliderOff();
        AttackColliderOn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeHealth(DAMAGE);
        }
    }
}
