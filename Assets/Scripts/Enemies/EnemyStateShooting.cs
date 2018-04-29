using UnityEngine;

public class EnemyStateShooting : EnemyState {

    public EnemyStateShooting(EnemyController enemy){
        _enemyRef = enemy;
    }
	
    public override void Update(){
        Vector3 enemyPlayerVector = FPSPlayerController.FPSPlayerInstance.transform.position - _enemyRef.transform.position;
        float distance = enemyPlayerVector.sqrMagnitude;
        float angle;
        Vector3 currentVelocity = Vector3.zero;

        enemyPlayerVector.y = 0f;
        angle = Vector3.Angle(enemyPlayerVector, _enemyRef.transform.forward);  

        if(distance <= _enemyRef.shootingDistance && Random.value <= _enemyRef.shotProbability){
            if(angle > _enemyRef.shootingAngle){
                Vector3 newForward = Vector3.SmoothDamp(_enemyRef.transform.forward, enemyPlayerVector, ref currentVelocity, _enemyRef.turnAroundSpeed);
                _enemyRef.transform.rotation = Quaternion.LookRotation(newForward);
            }
            
            if(angle < _enemyRef.shootingAngle)
                _enemyRef.Shoot(FPSCamera.CameraPosition);
        }        
    }
}
