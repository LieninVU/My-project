using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBaseForce = 1f;
    [SerializeField] private float knockBackTimerMax = 0.1f;

    private float _knockBackTimer;
    private Rigidbody2D rb;

    public bool IsGettingKnock { get; private set; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _knockBackTimer -= Time.deltaTime;
        if(_knockBackTimer < 0)
        {
            StopKnockBack();
        }
    }

    public void GetKnockBack(Transform damageSource)
    {
        IsGettingKnock = true;
        _knockBackTimer = knockBackTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBaseForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }

    public void StopKnockBack()
    {
        rb.linearVelocity = Vector2.zero;
        IsGettingKnock = false;
    }
}
