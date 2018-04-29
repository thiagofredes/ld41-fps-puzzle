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

    public float shotAccuracy;

    public int shotDamage;

    public float life;    

    public float turnAroundSpeed = 0.5f;

    public AudioSource audioSource;

    public AudioClip shotClip;

    public Transform shotOrigin;

    public LineRenderer shotLine;

    public Light shotMuzzle;

    private EnemyState _movementState;

    private EnemyState _shootState;

    private float _nextShotTime;

    private Coroutine _updateStatesCoroutine;


    void Start()
    {
        _nextShotTime = 0f;
        _updateStatesCoroutine = null;
    }

    public void SetMovementState(EnemyState newState){
        if(_updateStatesCoroutine != null)
            StopCoroutine(_updateStatesCoroutine);

        if(_movementState != null){
            _movementState.OnExit();
        }
        _movementState = newState;
        _movementState.OnEnter();

        _updateStatesCoroutine = StartCoroutine(UpdateStates());
    }

    public void SetShootState(EnemyState newState){
        if(_updateStatesCoroutine != null)
            StopCoroutine(_updateStatesCoroutine);

        if(_shootState != null){
            _shootState.OnExit();
        }
        _shootState = newState;
        _shootState.OnEnter();

        _updateStatesCoroutine = StartCoroutine(UpdateStates());
    }

    IEnumerator UpdateStates()
    {
        YieldInstruction endOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            if (!gamePaused && !gameEnded)
            {
                if(_movementState != null)
                    _movementState.Update();
                if(_shootState != null)
                    _shootState.Update();
            }
            else{
                _movementState.Stop();                
            }
            yield return endOfFrame;
        }
    }

    public void Shoot(Vector3 targetPosition)
    {
        if (Time.time >= _nextShotTime)
        {
            _nextShotTime = Time.time + timeBetweenShots;
            audioSource.PlayOneShot(shotClip);
            StartCoroutine(ShootCoroutine(targetPosition));            
        }
    }

    private IEnumerator ShootCoroutine(Vector3 targetPosition){
        Ray ray = new Ray(shotOrigin.position, (targetPosition - shotOrigin.position).normalized);
		RaycastHit raycastHit;
        float bulletSpeed = 5f;
		YieldInstruction endOfFrame = new WaitForEndOfFrame();

		shotLine.SetPosition(0, shotOrigin.position);
		shotLine.SetPosition(1, shotOrigin.position + shotOrigin.forward * bulletSpeed);		
		shotLine.enabled = true;

        shotMuzzle.enabled = true;

		while(!Physics.Raycast(ray.origin, ray.direction, out raycastHit, bulletSpeed, ~LayerMask.GetMask("Enemies"))){
			yield return endOfFrame;
			shotLine.SetPosition(0, ray.origin);
			ray.origin = ray.origin + ray.direction.normalized * bulletSpeed;			
			shotLine.SetPosition(1, ray.origin);
		}

        shotMuzzle.enabled = false;
		shotLine.enabled = false;
		shotLine.SetPosition(0, shotOrigin.position);
		shotLine.SetPosition(1, shotOrigin.position);

		FPSPlayerController player = raycastHit.transform.GetComponent<FPSPlayerController>();
		if(player != null){
            if(Random.value <= shotAccuracy)
			    player.Damage(shotDamage);
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
