using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseGameObject
{
    public event System.Action<EnemyController> EnemyKilled;

    public AfterDeathAction afterDeath;

    public NavMeshAgent navMeshAgent;

    public float shootingDistance;

    public float shootingAngle;

    public float timeBetweenShots;

    public float shotProbability;

    public int shotDamage;

    public float life;

    private EnemyState movementState;

    private EnemyState shootState;

    private float _nextShotTime;


    void Start()
    {
        movementState = new EnemyStateFollowing(this);
        shootState = new EnemyStateShooting(this);
        _nextShotTime = 0f;
        StartCoroutine(UpdateStates());
    }

    IEnumerator UpdateStates()
    {
        YieldInstruction endOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if (!gamePaused && !gameEnded)
            {
                movementState.Update();
                shootState.Update();
            }
            else{
                movementState.Stop();                
            }
            yield return endOfFrame;
        }
    }

    public void Shoot(Vector3 targetPosition)
    {
        if (Time.time >= _nextShotTime)
        {
            _nextShotTime = Time.time + timeBetweenShots;
            FPSPlayerController.FPSPlayerInstance.Damage(shotDamage);
        }
    }

    public void Damage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            afterDeath.Execute(transform.position);

            if (EnemyKilled != null)
                EnemyKilled(this);
            
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
