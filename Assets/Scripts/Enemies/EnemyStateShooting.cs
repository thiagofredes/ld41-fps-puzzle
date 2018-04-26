using UnityEngine;

public class EnemyStateShooting : EnemyState {

    public EnemyStateShooting(EnemyController enemy){
        enemyRef = enemy;
    }
	
    public override void Update(){
        Vector3 enemyPlayerVector = FPSPlayerController.FPSPlayerInstance.transform.position - enemyRef.transform.position;
        float distance = enemyPlayerVector.sqrMagnitude;
        float angle;
        Vector3 currentVelocity = Vector3.zero;

        enemyPlayerVector.y = 0f;
        angle = Vector3.Angle(enemyPlayerVector, enemyRef.transform.forward);  

        if(distance <= enemyRef.shootingDistance && Random.value <= enemyRef.shotProbability){
            if(angle > enemyRef.shootingAngle){
                Vector3 newForward = Vector3.SmoothDamp(enemyRef.transform.forward, enemyPlayerVector, ref currentVelocity, enemyRef.turnAroundSpeed);
                enemyRef.transform.rotation = Quaternion.LookRotation(newForward);
            }
            
            if(angle < enemyRef.shootingAngle)
                enemyRef.Shoot(FPSCamera.CameraPosition);
        }        
    }
}
