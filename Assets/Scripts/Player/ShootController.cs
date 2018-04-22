using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	
	public Transform shotOrigin;

	public float timeBetweenShots;

	public float bulletSpeed;

	public float bulletDamage;

	private float nextShotTime;


	void Awake(){
		nextShotTime = 0f;
	}
	public void Shoot(){
		if(Time.time >= nextShotTime){
			nextShotTime = Time.time + timeBetweenShots;
			StartCoroutine(BulletCoroutine());
		}
	}

	private IEnumerator BulletCoroutine(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));
		RaycastHit raycastHit;
		YieldInstruction endOfFrame = new WaitForEndOfFrame();		

		while(!Physics.Raycast(ray.origin, ray.direction, out raycastHit, bulletSpeed, ~LayerMask.GetMask("Player", "Weapon"))){
			yield return endOfFrame;
			ray.origin = ray.origin + ray.direction.normalized * bulletSpeed;
			Debug.DrawRay(ray.origin, ray.direction * bulletSpeed, Color.magenta);
		}

		EnemyController enemy = raycastHit.transform.GetComponent<EnemyController>();
		if(enemy != null){
			enemy.Damage(bulletDamage);
		}
	}	
}
