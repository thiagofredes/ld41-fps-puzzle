using UnityEngine;

public class ExecutePuzzleStepAfterDeathAction : AfterDeathAction
{
    public PuzzlePiece piece;    


    private PuzzleSolver _solver;

    void Awake(){
        _solver = PuzzleSolver.PuzzleSolverInstance;
    }

    public override void Execute(Vector3 spawnPosition)
    {
        _solver.PutPiece(piece);
    }
}