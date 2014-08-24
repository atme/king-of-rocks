using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	private GameObject[] towers;
	private List<AI> ai = new List<AI>();
	private int[] teamTowers = new int[5];
	private Hashtable towerScripts = new Hashtable();
	public Object aiPrefab;

	// Use this for initialization
	void Start () {
		towers = GameObject.FindGameObjectsWithTag("Tower");
		foreach (GameObject tower in towers) {
			Tower towerScript = tower.GetComponent<Tower>();
			towerScripts.Add(towerScript.GetInstanceID(), towerScript);
			teamTowers[towerScript.team] = towerScript.GetInstanceID();
		}
		
		GameObject aiGameObject;
		AI aiComponent;
		for (int key = 0; key < teamTowers.Length; ++key) {
			if (key < 2 || teamTowers[key] == 0)
				continue;
			aiGameObject = (GameObject) Instantiate(aiPrefab);
			aiComponent = aiGameObject.GetComponent<AI>();
			aiComponent.setTeam(key);
			ai.Add(aiComponent);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
//	public void GetTeamTowers() {
//		return teamTowers;
//	}
	
}
