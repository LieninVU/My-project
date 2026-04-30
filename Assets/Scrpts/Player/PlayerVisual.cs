using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private FlashBlink flashBlink;
    private const String IS_RUN = "IsRun";
    private const String IS_DIE = "IsDie";

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += PlayerOnPlayerDeath;
        flashBlink = GetComponent<FlashBlink>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUN, Player.Instance.isRunning());
        if(Player.Instance.isAlive()) AjustPlayerFacingDirection();
    }


    private void PlayerOnPlayerDeath(object sender, EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        flashBlink.StopBlinking();
    }

    private void AjustPlayerFacingDirection()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPos = Player.Instance.GetPlayerPosition();
        if (mousePos.x < playerPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }

    }
    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= PlayerOnPlayerDeath;
    }
}
