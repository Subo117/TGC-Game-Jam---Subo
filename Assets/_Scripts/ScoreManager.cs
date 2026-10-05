using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int currentOrb = 0;

    public int CurrentOrb => currentOrb;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        Instance = this;
        currentOrb = 0;
    }

    public void AddScore(int score)
    {
        currentOrb += score;

        OnScoreChanged?.Invoke(currentOrb);
    }
}
