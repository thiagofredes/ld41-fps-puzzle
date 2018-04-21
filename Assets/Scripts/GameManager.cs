using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
	private static bool paused = false;

	private static bool gameOver = false;

	public static event Action GamePaused;

	public static event Action<bool> GameEnded;

	public static event Action GameResumed;

	public static void Pause ()
	{
		if (GamePaused != null)
			GamePaused ();
	}

	public static void Resume ()
	{
		if (GameResumed != null)
			GameResumed ();
	}

	public static void EndGame (bool success)
	{
		if (GameEnded != null)
			GameEnded (success);
		gameOver = !success;
	}

	void Update ()
	{
		if (!gameOver) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (!paused) {
					Pause ();
				} else {
					Resume ();
				}
				paused = !paused;
			}
		}
	}
}
