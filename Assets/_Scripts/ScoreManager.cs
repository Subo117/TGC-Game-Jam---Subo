using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int currentOrb = 0;
    
    public int CurrentOrb => currentOrb;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        this.currentOrb += score;
        OnScoreChanged?.Invoke(currentOrb);
    }


}
