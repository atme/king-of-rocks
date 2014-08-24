using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	private int team;
	private Game game;
	private int[] teamTowers = new int[5];

	// Use this for initialization
	void Start () {
		GameObject mainCamera = GameObject.Find ("Main Camera");
		game = mainCamera.GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
//		teamTowers = game.GetTeamTowers();
		//if (teamTowers[1].Length) {
		
		//}
	}
	
	public void setTeam(int _team) {
		team =  _team;
	}
	
}
