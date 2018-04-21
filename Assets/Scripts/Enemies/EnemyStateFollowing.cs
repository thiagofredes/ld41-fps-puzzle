using UnityEngine;

public class EnemyStateFollowing : EnemyState {

	public EnemyStateFollowing(EnemyController enemy){
		enemyRef = enemy;
	}

	public override void Update(){
		enemyRef.navMeshAgent.SetDestination(FPSPlayerController.FPSPlayerInstance.transform.position);
	}
}
