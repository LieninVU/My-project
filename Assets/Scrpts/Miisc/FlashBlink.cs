using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damageObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDration = 0.1f;

    private float blinkTimer;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        isBlinking = true;

    }

    private void Start()
    {
        if(damageObject is Player player)
        {
            player.OnFlashBlink += PlayerOnFlashBlink;
        }

    }

    private void Update()
    {
        if (isBlinking)
        {
            blinkTimer -= Time.deltaTime;
            if(blinkTimer < 0)
            {
                SetDefaultMaterial();
            }
        }
    }

    public void StopBlinking()
    {
        SetDefaultMaterial();
        isBlinking = false;
    }
    private void PlayerOnFlashBlink(object sender, System.EventArgs e)
    {
        SetBlinkingMaterial();
    }

    private void SetDefaultMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    private void SetBlinkingMaterial()
    {
        blinkTimer = blinkDration;
        spriteRenderer.material = blinkMaterial;
    }

    private void OnDestroy()
    {
        if(damageObject is Player player)
        {
            player.OnFlashBlink -= PlayerOnFlashBlink;
        }
    }
}
