using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyVisual : MonoBehaviour
{
    SpriteRenderer  _spriteRenderer;


    [SerializeField] private Enemy_AI enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private GameObject _enemyShadow;
    private Animator animator;
    private const string IS_RUN = "IsRun";
    private const string ATTACK = "Attack";
    private const string TAKE_DAMAGE = "TakeDamage";
    private const string IS_DIE = "IsDie";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += EnemyEntityOnTakeHit;
        _enemyEntity.OnDeath += EnemyEntityOnDeath;
    }


    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit -= EnemyEntityOnTakeHit;
        _enemyEntity.OnDeath -= EnemyEntityOnDeath;
    }


    private void Update()
    {
        animator.SetBool(IS_RUN, enemyAI.isRun);
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, enemyAI.GetCurrnetAnimationSpeedMultiplier());
    }
    public void TriggerAttackAnimationTurnOff()
    {
        _enemyEntity.PolygonColiderOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        _enemyEntity.PolygonColiderOn();
    }

    private void EnemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
    private void EnemyEntityOnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKE_DAMAGE);
    }
    private void EnemyEntityOnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = -1;
        _enemyShadow.SetActive(false);
    }

}
