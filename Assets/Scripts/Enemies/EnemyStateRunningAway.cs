using UnityEngine;

public class EnemyStateRunningAway : EnemyState {

	private Transform[] _wayPoints;

	private bool _movingToWaypoint;

	public EnemyStateRunningAway(EnemyController enemy, Transform[] wayPoints){
		_enemyRef = enemy;
		_wayPoints = wayPoints;
	}

	public override void Update()
	{
		if(!_movingToWaypoint){
			_enemyRef.navMeshAgent.SetDestination(_wayPoints[ChooseWaypoint()].position);
			_movingToWaypoint = true;
		}
		else{
			if(_enemyRef.navMeshAgent.remainingDistance < 1f){
				_movingToWaypoint = false;
			}
		}
	}

	public override void Stop(){
		_enemyRef.navMeshAgent.isStopped = true;		
	}

	private int ChooseWaypoint(){
		float largestDistance = 0f;
		int wayPointIndex = Random.Range(0, _wayPoints.Length);

		for(int wp = 0; wp<_wayPoints.Length; wp++){
			float pDistance = Vector3.Distance(FPSPlayerController.FPSPlayerInstance.transform.position, _wayPoints[wp].position);
			if(pDistance > largestDistance){
				largestDistance = pDistance;
				wayPointIndex = wp;
			}
		}
		return wayPointIndex;
	}
}
