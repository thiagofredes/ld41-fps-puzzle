using UnityEngine;

public class EnemyStateFollowing : EnemyState {

	public EnemyStateFollowing(EnemyController enemy){
		_enemyRef = enemy;
	}

	public override void Update(){
		_enemyRef.navMeshAgent.SetDestination(FPSPlayerController.FPSPlayerInstance.transform.position);
	}

	public override void Stop(){
		_enemyRef.navMeshAgent.isStopped = true;		
	}
}
