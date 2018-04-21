using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public NavMeshAgent navMeshAgent;

	public float shootingDistance;

	public float shootingAngle;

	public float timeBetweenShots;

	public float shotProbability;

	public float shotDamage;

	private EnemyState movementState;

	private EnemyState shootState;

	private float _nextShotTime;


	void Start(){
		movementState = new EnemyStateFollowing(this);
		shootState = new EnemyStateShooting(this);
		_nextShotTime = 0f;
	}

	void Update(){
		movementState.Update();
		shootState.Update();
	}

	public void Shoot(Vector3 targetPosition){
		if(Time.time >= _nextShotTime){
			_nextShotTime = Time.time + timeBetweenShots;
			FPSPlayerController.FPSPlayerInstance.Damage(shotDamage);
		}
	}
}
