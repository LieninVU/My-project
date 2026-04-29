using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private float _damageRecoveryTime = 0.5f;
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    private Rigidbody2D rb;
    private float minMovementSpeed = 0.1f;
    private bool isRun = false;
    private bool _canTakeDamage = false;
    private bool _isAlive;
    private int _currentHealth;
    private KnockBack _knockBack;
    Vector2 inputVector;
    
    
    public static Player Instance { get; private set; }
    
    
    
    
    
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _canTakeDamage = true;
        _isAlive = true;
        GameInput.Instance.OnPlayerAttack += PlayerOnPlayerAttack;
    }
    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVEctor();
        if (_knockBack.IsGettingKnock) { return; }
        HandleMovement();
    }

    public bool isRunning()
    {
        return isRun;
    }

    public Vector3 GetPlayerPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth -= damage);
            Debug.Log(_currentHealth);
            _knockBack.GetKnockBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DamageRocoveryRoutine());
        }
        DetectDetah();
    }

    public bool isAlive() {  return _isAlive; }

    private void DetectDetah()
    {
        if(_currentHealth == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBack();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            //Destroy(this.gameObject); 
        }
    }

    private IEnumerator DamageRocoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
    private void PlayerOnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }



    private void HandleMovement()
    {
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (Speed * Time.fixedDeltaTime));


        if (Mathf.Abs(inputVector.x) > minMovementSpeed || Mathf.Abs(inputVector.y) > minMovementSpeed)
        {
            isRun = true;
        }
        else
        {
            isRun= false;
        }
    }


}
