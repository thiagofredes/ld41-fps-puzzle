using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseGameObject
{

    public Text lifeText;

    public Text timerText;

	public Text gameOverText;


    void OnEnable()
    {
        base.OnEnable();
        GameManager.Tick += OnTick;
        GameManager.GameEnded += OnGameEnded;
        FPSPlayerController.LifeUpdate += OnLifeUpdate;
    }

    void OnDisable()
    {
        base.OnDisable();
        GameManager.Tick -= OnTick;
        GameManager.GameEnded -= OnGameEnded;
        FPSPlayerController.LifeUpdate -= OnLifeUpdate;
    }

    void Awake()
    {
        timerText.text = "";
        lifeText.text = "";
		gameOverText.enabled = false;
    }

    private void OnTick(float tickSeconds)
    {
        int minutes = Mathf.FloorToInt(tickSeconds / 60);
        int seconds = Mathf.FloorToInt(tickSeconds % 60);
        timerText.text = string.Format("Time remaining: {0}:{1:00}", minutes, seconds);
    }

    private void OnLifeUpdate(int newLife)
    {
        lifeText.text = string.Format("Life: {0}", newLife);
    }

    protected override void OnGameEnded(bool success)
    {
        base.OnGameEnded(success);
        if(success){
			gameOverText.text = "SYSTEM OVERRIDEN. CONGRATULATIONS, THREAD NUMBER 41.";
		}
		else{
			gameOverText.text = "SYSTEM OVERRIDE COMPROMISED. YOU'VE FAILED.";
		}
        gameOverText.enabled = true;
    }
}
