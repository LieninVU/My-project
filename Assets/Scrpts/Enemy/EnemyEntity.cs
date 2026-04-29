using System;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyEntity : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    [SerializeField] private EnemySO _enemySO;
    //[SerializeField] private int maxHealth;
    private int currenHealth;
    private CapsuleCollider2D _capsuleCollider2D;
    private PolygonCollider2D _polygonColider2D;
    private BoxCollider2D _boxCollider2D;
    private Enemy_AI _enemyAI;

    private void Awake()
    {
        _polygonColider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<Enemy_AI>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        currenHealth = _enemySO.enemyHealth;
        PolygonColiderOff();
    }
    public void PolygonColiderOff()
    {
        _polygonColider2D.enabled = false;
    }

    public void PolygonColiderOn()
    {
        _polygonColider2D.enabled = true;
    }


    public void TakeHealth(int damage)
    {
        currenHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectedDeath();
    }

    private void DetectedDeath()
    {
        if(currenHealth <= 0)
        {
            _capsuleCollider2D.enabled = false;
            _boxCollider2D.enabled = false;
            _polygonColider2D.enabled = false;
            OnDeath?.Invoke(this, EventArgs.Empty);
            _enemyAI.SetDeathState();
            //Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _enemySO.enemyDanageAmount);
        }
    }


}
