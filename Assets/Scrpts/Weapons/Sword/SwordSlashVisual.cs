using System;
using UnityEngine;

public class SwordSlashVisual : MonoBehaviour
{
    [SerializeField] Sword sword;
    private Animator animator;
    private const string ATTACK = "Attack";

    private void Awake()
    {
        animator= GetComponent<Animator>();
    }

    private void Start()
    {
        sword.onSwordSwing += SwordOnSwordSwing;
    }

    private void SwordOnSwordSwing(object sender, EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
}
