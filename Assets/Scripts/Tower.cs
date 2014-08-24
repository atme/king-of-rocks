using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public int ammo;
	public int team;
	public Object ammoPrefab;
	private float increaseAmmoTime = 0.5f;
	private TextMesh ammoCount;
	private int selected = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating("IncreaseAmmo", increaseAmmoTime, increaseAmmoTime);
		ammoCount = gameObject.GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		ammoCount.text = ammo.ToString();
	}
	
	void IncreaseAmmo() {
		ammo++;
	}
	
	void OnMouseDown() {
		Select ();
	}

	void OnTouchDown() {
		Select ();
	}

	void Select() {
		GameObject selectedGameObject = GameObject.FindGameObjectWithTag("SelectedTower");
		
		if (selectedGameObject != null && selectedGameObject != gameObject) {
			Tower tower = selectedGameObject.GetComponent<Tower>();
			tower.SendAmmo (gameObject);
			return;
		}
		
		if (team != 0)
			return;
			
		selected++;
		gameObject.tag = "SelectedTower";
		if (selected == 3)
			Unselected();
	}
	
	void Unselected() {
		selected = 0;
		gameObject.tag = "Tower";
	}
	
	public void SendAmmo(GameObject tower) {
		GameObject ammoObject = Instantiate(
			ammoPrefab, 
			new Vector3(transform.position.x, transform.position.y, -1), 
			Quaternion.identity
		) as GameObject;
		Ammo ammoClass = ammoObject.GetComponent<Ammo>();
		
		int sendedAmmo = 0;
		
		if (selected == 1) {
			sendedAmmo = Mathf.FloorToInt(ammo / 2);
			ammo = Mathf.CeilToInt(ammo / 2);
		}
		
		if (selected == 2) {
			sendedAmmo = ammo;
			ammo = 0;
		}
		
		ammoClass.Set(gameObject, tower, sendedAmmo);
		
		Unselected();
	}
	
	public void AddAmmo(int receivedAmmo) {
		ammo += receivedAmmo;
	}
	
	public void Attack(int damage, int enemyTeam) {
		ammo -= damage;
		if (ammo < 0) {
			team = enemyTeam;
			ammo = -ammo;
		}
	}
}
