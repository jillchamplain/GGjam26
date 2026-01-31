using UnityEngine;
using TMPro;
public class SetManager : MonoBehaviour
{
    [SerializeField] int p1Wins;
    [SerializeField] int p2Wins;

    [SerializeField] TextMeshProUGUI p1TF;
    [SerializeField] TextMeshProUGUI p2TF;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public delegate void PlayerWonSet(Player player);
    public static event PlayerWonSet playerWonMatch;

    private void OnEnable()
    {
        ScoreManager.playerWonGame += WinGame;

    }

    private void OnDisable()
    {
        ScoreManager.playerWonGame -= WinGame;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleWins()
    {
        HandleWin(p1Wins, p1TF);
        HandleWin(p2Wins, p2TF);

        if (p1Wins >= 2)
        {
            playerWonMatch?.Invoke(Player.PLAYER_ONE);
            Debug.Log("p1 Wins");
        }
        else if (p2Wins >= 2)
        {
            playerWonMatch?.Invoke(Player.PLAYER_TWO);
            Debug.Log("p2 Wins");
;        }
    }

    void HandleWin(int score, TextMeshProUGUI tf)
    {
        tf.text = score.ToString();
    }

    void WinGame(Player player)
    {
        switch(player)
        {
            case Player.PLAYER_ONE:
                p1Wins++;
                HandleWins();
                break;
            case Player.PLAYER_TWO:
                p2Wins++; 
                HandleWins();
                break;
        }
    }
}
