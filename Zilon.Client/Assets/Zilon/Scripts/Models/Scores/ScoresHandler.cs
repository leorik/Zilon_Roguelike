﻿using Assets.Zilon.Scripts.Services;

using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

public class ScoresHandler : MonoBehaviour
{
    public ScoresTableRow ScoresTableRowPrefab;
    public Transform ScoreRecordParent;

    [Inject]
    private readonly ScoreStorage _scoreStorage;

    public void Awake()
    {
        var scoreRecords = _scoreStorage.ReadScores();

        foreach (var record in scoreRecords)
        {
            var row = Instantiate(ScoresTableRowPrefab, ScoreRecordParent);
            row.Init(record.Number, record.Name, record.Scores, "[not impl]", "[not impl]");
        }
    }

    public void ToMainMenuButton_Handler()
    {
        SceneManager.LoadScene("title");
    }
}