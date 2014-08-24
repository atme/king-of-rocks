using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
	
	private TextMesh ammoCount;
	private int ammo;
	private GameObject fromTower;
	private GameObject toTower;
	private float speed = 2;
	private Tower toTowerComponent;
	private Tower fromTowerComponent;

	// Use this for initialization
	void Start () {
		ammoCount = gameObject.GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, toTower.transform.position, step);
		ammoCount.text = ammo.ToString();
	}
	
	public void Set(GameObject _fromTower, GameObject _toTower, int _ammo) {
		ammo = _ammo;
		fromTower = _fromTower;
		toTower = _toTower;
		fromTowerComponent = fromTower.GetComponent<Tower>();
		toTowerComponent = toTower.GetComponent<Tower>();
	}
	
	void OnTriggerStay2D(Collider2D collider) {
		if (collider == toTower.collider2D) {
			if (IsAttack())
				toTowerComponent.Attack(ammo, fromTowerComponent.team);
			else
				toTowerComponent.AddAmmo(ammo);
			Destroy(gameObject);
		}
		
		if (collider.name == "Ammo(Clone)") {
			Ammo colliderAmmo = collider.GetComponent<Ammo>();
			if (
				fromTower == colliderAmmo.GetToTower() && 
				toTower == colliderAmmo.GetFromTower() && 
				IsAttack()
			) {
				int deltaAmmo = ammo - colliderAmmo.GetAmmo();
				if (deltaAmmo <= 0)
					Destroy(gameObject);
				else
					ammo = deltaAmmo;
			}
		}
		
	}
		
	IEnumerator doAmmoCollision(Ammo colliderAmmo) {
		print(Time.time + "aa");
		yield return new WaitForSeconds(5);
		print(Time.time);
		
	}
	
	bool IsAttack() {
		return toTowerComponent.team != fromTowerComponent.team;
	}
	
	public GameObject GetFromTower() {
		return fromTower;
	} 
	
	public GameObject GetToTower() {
		return toTower;
	} 
	
	public int GetAmmo() {
		return ammo;
	} 
}
