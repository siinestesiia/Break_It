using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bricksContainer;
    int bricksRemaining;
    int initialBricks;

    OutOfBoundsTrigger triggerScript;
    
    [Header ("- Scriptable Objects -")]
    [SerializeField] ScoreSystem scoreSystem; // Scriptable Object.

    [Header ("- Ui Elements -")]
    // UI elements:
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;

    // Score System values
    int score;
    int lives;
    bool hasWon;

    
    void Start()
    {
        GetComponents();
        SubscribeToOnBallOutOfBounds();

        ScoreSystemValues();
        initialBricks = bricksContainer.transform.childCount;
        livesText.text = "Lives: " + lives.ToString(); // Show first value.

        // Win-lose message
        winText.enabled = false;
        loseText.enabled = false;
    }

    void Update()
    {
        // Debug.Log($"Lives: {lives}.\n" + $"Score: {score}.\n" + $"Has won? {hasWon}.");
        BricksCounter();

    }

    
    void BricksCounter()
    {
        
        bricksRemaining = bricksContainer.transform.childCount;

        int bricksDestroyed = initialBricks - bricksRemaining;
        ScorePoints(bricksDestroyed);
        
        if (bricksDestroyed == initialBricks)
        {
            ProcessWinning();
        }
    }

    void ScorePoints(int bricksDestroyed)
    {
        int pointsPerHit = 5;

        score = bricksDestroyed * pointsPerHit; 
        scoreText.text = "Score: " + score.ToString();
        Debug.Log("Total Points: " + score);
    }

    void ProcessWinning()
    {
            // Winning Event Here.
            winText.enabled = true;
            Debug.Log("You win!!");
    }

    void ProcessLoosing()
    {
        Debug.Log("You loose.");
        loseText.enabled = true;

        // Create Event
    }

    void SubscribeToOnBallOutOfBounds()
    {
        if (triggerScript == null)
        {
            Debug.LogError("OutOfBounds Trigger not found!");
        }
        else
        {
            triggerScript.OnBallOutOfBounds += HandleBallOutOfBounds;
        }
    }
    
    void HandleBallOutOfBounds()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();


        if (lives <= 0)
        {
            ProcessLoosing();
        }
    }
    
    void ScoreSystemValues()
    {
        scoreSystem = ScriptableObject.CreateInstance<ScoreSystem>();
        lives = scoreSystem.lives;
        score = scoreSystem.score;
        hasWon = scoreSystem.hasWon;
    }

    void GetComponents()
    {
        triggerScript = FindObjectOfType<OutOfBoundsTrigger>();
    }
}
