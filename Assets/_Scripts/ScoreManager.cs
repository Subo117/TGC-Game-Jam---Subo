using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int currentOrb = 0;
    
    public int CurrentOrb => currentOrb;

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
        Debug.Log($"Current Orb : {this.currentOrb}");
    }

    public int GetScore()
    {
        return currentOrb;
    }
}
