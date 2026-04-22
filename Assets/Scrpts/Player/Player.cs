using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    private Rigidbody2D rb;
    private float minMovementSpeed = 0.1f;
    private bool isRun = false;
    Vector2 inputVector;
    public static Player Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += PlayerOnPlayerAttack;
    }

    private void PlayerOnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVEctor();
        HandleMovement();
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

    public bool isRunning()
    {
        return isRun;
    }

    public Vector3 GetPlayerPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
}
