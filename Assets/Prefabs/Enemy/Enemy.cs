using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IBehaviorTreeInterface, ITeamInterface
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComp;
    [SerializeField] BehaviorTree behaviorTree;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int TeamID = 2;
    
    Vector3 prevPos;

    public int GetTeamID()
    {
        return TeamID;
    }
    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(healthComponent!=null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
        perceptionComp.onPerceptionTargetChanged += TargetChanged;
        prevPos = transform.position;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if(sensed)
        {
            behaviorTree.Blackboard.SetOrAddData("Target", target);
        }
        else
        {
            behaviorTree.Blackboard.SetOrAddData("LastSeenLoc", target.transform.position);
            behaviorTree.Blackboard.RemoveBlackboardData("Target");
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        
    }

    private void StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TriggerDeathAnimation()
    {
        if(animator!= null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        if (movementComponent == null) return;

        Vector3 posDelta = transform.position - prevPos;
        float speed = posDelta.magnitude / Time.deltaTime;
        Animator.SetFloat("Speed", speed);
        prevPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(behaviorTree && behaviorTree.Blackboard.GetBlackboardData("Target", out GameObject target))
        {
            Vector3 drawTragetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTragetPos, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTragetPos);
        }
    }

    public void RotateTowards(GameObject target, bool vertialAim = false)
    {
        Vector3 AimDir = target.transform.position - transform.position;
        AimDir.y = vertialAim ? AimDir.y : 0;
        AimDir = AimDir.normalized;

        movementComponent.RotateTowards(AimDir);
    }

    public virtual void AttackTarget(GameObject target)
    {
        //override in child
    }
}
