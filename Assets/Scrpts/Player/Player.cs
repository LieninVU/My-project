using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float damageRecoveryTime = 0.5f;
    [SerializeField] private int dashSpeedMuliplier = 4;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float dashCoolDownTime = 0.5f;    
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;
    public event Action<int, int> OnHealthChange;
    public event Action OnShowRestartMenu;
    private Rigidbody2D rb;
    private readonly float minMovementSpeed = 0.1f;
    private bool isRun = false;
    private bool _canTakeDamage = false;
    private bool _isAlive;
    private bool isDashing = false;
    private int _currentHealth;
    private KnockBack _knockBack;
    Vector2 inputVector;
    private Camera _mainCamera;
    private float _standartSpeed;
    private float _dashSpeed;
    
    public static Player Instance { get; private set; }
    
    
    
    
    
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();
        _mainCamera = Camera.main;
        _standartSpeed = Speed;
        _dashSpeed = Speed * dashSpeedMuliplier;
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        _canTakeDamage = true;
        _isAlive = true;
        GameInput.Instance.OnPlayerAttack += PlayerOnPlayerAttack;
        GameInput.Instance.OnPlayerDash += PlayerOnPlayerDash;
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
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
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
            OnHealthChange?.Invoke(_currentHealth, maxHealth);
        }
        DetectDetah();
    }

    public bool isAlive() {  return _isAlive; }

    public int GetCurrentHealth() { return _currentHealth; }
    public int GetMaxHealth() { return maxHealth; }

    private void DetectDetah()
    {
        if(_currentHealth == 0 && _isAlive)
        {
            _isAlive = false;
            _knockBack.StopKnockBack();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            OnHealthChange?.Invoke(_currentHealth, maxHealth);
            OnShowRestartMenu?.Invoke();
        }
    }

    private IEnumerator DamageRocoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }
    private void PlayerOnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }
    private void PlayerOnPlayerDash(object sender, EventArgs e)
    {
        Dash();
    }

    private void Dash()
    {
        if (!isDashing) StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        Speed = _dashSpeed;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        Speed = _standartSpeed;
        yield return new WaitForSeconds(dashCoolDownTime);
        isDashing = false;
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
    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= PlayerOnPlayerAttack;
    }


}
