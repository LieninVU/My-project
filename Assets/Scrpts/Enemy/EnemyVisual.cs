using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private NPC_AI npcAI;
    private Animator animator;
    private const string IS_RUN = "IsRun";
    private const string ATTACK = "Attack";
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string IS_DIE = "IsDie";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUN, npcAI.isRun);
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, npcAI.GetCurrnetAnimationSpeedMultiplier());
    }
}
