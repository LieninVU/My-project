using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _damageObject;
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

        if(_damageObject is Player)
        {
            (_damageObject as Player).OnFlashBlink += PlayerOnFlashBlink;
        }
    }

    private void PlayerOnFlashBlink(object sender, System.EventArgs e)
    {
        SetBlinkingMaterial();
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

    private void SetDefaultMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    private void SetBlinkingMaterial()
    {
        blinkTimer = blinkDration;
        spriteRenderer.material = blinkMaterial;
    }
}
