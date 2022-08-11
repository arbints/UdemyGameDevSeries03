using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComp;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if(healthComponent!=null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
        perceptionComp.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if(sensed)
        {
            this.target = target;
        }
        else
        {
            this.target = null;
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth)
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
        
    }

    private void OnDrawGizmos()
    {
        if(target!=null)
        {
            Vector3 drawTragetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTragetPos, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTragetPos);
        }
    }
}
