using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] int p1Score;
    [SerializeField] int p2Score;

    [SerializeField] TextMeshProUGUI p1TF;
    [SerializeField] TextMeshProUGUI p2TF;
    [SerializeField] TextMeshProUGUI dueceTF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public delegate void PlayerWonGame(Player player);
    public static event PlayerWonGame playerWonGame;

    private void OnEnable()
    {
        Ball.ballBouncedTwice += HandleScoreIncrease;
    }

    private void OnDisable()
    {
        Ball.ballBouncedTwice -= HandleScoreIncrease;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleScoreIncrease(Player player)
    {
        switch(player)
        {
            case Player.PLAYER_ONE:
                IncreasePlayer1Score();
                break;
            case Player.PLAYER_TWO:
                IncreasePlayer2Score();
                break;
        }
    }

    public void IncreasePlayer1Score()
    {
		p1Score++;
		HandleScores();
	}

    public void IncreasePlayer2Score()
    {
		p2Score++;
		HandleScores();
	}

    void ResetScores()
    {
        p1Score = 0;
        p2Score = 0;
        HandleScores();
    }

    void HandleScores()
    {
        p1TF.gameObject.SetActive(true);
        p2TF.gameObject.SetActive(true);
		dueceTF.gameObject.SetActive(false);

		HandleScore(p1Score, p1TF);
        HandleScore(p2Score, p2TF);


		if (p1Score >= 4 && p1Score >= p2Score + 2)
		{
			p1TF.text = "Win";
            //p1  Wins
            playerWonGame?.Invoke(Player.PLAYER_ONE);
            ResetScores();
			return;
		}

		else if (p2Score >= 4 && p2Score >= p1Score + 2)
        {
            p2TF.text = "Win";
            //p2 Wins
            playerWonGame?.Invoke(Player.PLAYER_TWO);
            ResetScores();
            return;
        }

		if (p1Score == p2Score) //All
		{
			p2TF.text = "All";
		}

		if (p1Score >= 3 && p2Score >= 3) 
        {
            if (p1Score >= p2Score + 1) //Adv
            {
                p1TF.text = "Adv";
                return;
            }
            else if(p2Score >= p1Score + 1)
            {
                p2TF.text = "Adv";
                return;
            }
			//Duece
			p1TF.gameObject.SetActive(false);
            p2TF.gameObject.SetActive(false);
            dueceTF.gameObject.SetActive(true);
        }
    }

    void HandleScore(int score, TextMeshProUGUI tf)
    {
        switch(score)
        {
            case 0:
                tf.text = "Love";
                break;
            case 1:
                tf.text = "15";
                break;
            case 2:
                tf.text = "30";
                break;
            default:
                tf.text = "40";
                break;
        }
    }

    bool ScoreCheck(int score1, int score2)
    {
        return score1 > score2 + 2;
    }
}
