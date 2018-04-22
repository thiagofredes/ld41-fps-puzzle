using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : BaseGameObject
{

    public int maxLevels;

    public float timeBetweenTips;

    public PuzzlePiece[] possiblePieces;

    public EnemySpawner enemySpawner;

    public AudioSource audioSource;

    private PuzzlePiece[] _levelPieces;

    private int _totalSteps;

    private int _currentStep;

    private int _currentLevel;

    private static PuzzleSolver _instance;

    public static PuzzleSolver PuzzleSolverInstance
    {
        get
        {
            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _currentLevel = 1;
        _totalSteps = 3;
        SetupLevel();
        StartCoroutine(GiveTips());
        enemySpawner.Spawn(_levelPieces);
    }

    private void SetupLevel()
    {
        _levelPieces = new PuzzlePiece[_totalSteps];
        for (int p = 0; p < _totalSteps; p++)
        {
            _levelPieces[p] = possiblePieces[Random.Range(0, possiblePieces.Length)];
        }
        _currentStep = 0;
    }

    private IEnumerator GiveTips()
    {
        YieldInstruction waitTimeForTip = new WaitForSeconds(timeBetweenTips);

        while (true)
        {
            if (!gameEnded && !gamePaused)
            {
                foreach (PuzzlePiece p in _levelPieces)
                {
                    Shader.SetGlobalColor("_FlashColor", p.firewallRule);
                    audioSource.PlayOneShot(p.sound);
                    yield return FlashColor();
                }
            }
            yield return waitTimeForTip;
        }
    }

    private IEnumerator FlashColor()
    {
        float t = 0;
        float f = 0f;
        float flashTime = 1.5f;
        YieldInstruction endOfFrame = new WaitForEndOfFrame();

        while (t < flashTime)
        {
            if (!gameEnded && !gamePaused)
            {
                t += Time.deltaTime;
                f = Mathf.Sin((t / flashTime) * 3.14f);
                Shader.SetGlobalFloat("_FlashMultiplier", f);
            }
            yield return endOfFrame;
        }
        Shader.SetGlobalFloat("_FlashMultiplier", 0f);
    }

    public void PutPiece(PuzzlePiece piece)
    {
        if (_levelPieces[_currentStep].firewallRule == piece.firewallRule)
            Solve();
        else
            Punish();

        if (_currentStep == _totalSteps)
        {
            _currentLevel++;
            if (_currentLevel >= maxLevels)
            {
                GameManager.EndGame(true);
            }
            else
            {
                StopAllCoroutines();
                Shader.SetGlobalFloat("_FlashMultiplier", 0f);
                _totalSteps = Random.Range(2 + _currentLevel, 3 + _currentLevel);
                SetupLevel();
                enemySpawner.Spawn(_levelPieces);
                StartCoroutine(GiveTips());
            }
        }
    }

    private void Solve()
    {
        _currentStep++;
    }

    private void Punish()
    {
        GameManager.DiminishTimer();
    }
}
