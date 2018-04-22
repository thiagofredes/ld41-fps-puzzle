using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour {

	public int steps;

	public PuzzlePiece[] expectedPieces;

	private int _currentStep;

	private static PuzzleSolver _instance;

	public static PuzzleSolver PuzzleSolverInstance {
		get {
			return _instance;
		}
	}


	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		_currentStep = 0;
	}
	
	public void PutPiece(PuzzlePiece piece){
		if(expectedPieces[_currentStep].firewallRule == piece.firewallRule)
			Solve();
		else
			Punish();
	}

	private void Solve(){
		_currentStep ++;
	}

	private void Punish(){
		Debug.Log("WRONG!");
	}
}
