using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor;
public class SetGameManager : MonoBehaviour
{
    [SerializeField] int p1Wins;
    [SerializeField] int p2Wins;

    [SerializeField] Player server;
    [SerializeField] GameObject ballPF;
    [SerializeField] GameObject p1BallSpawn;
    [SerializeField] GameObject p2BallSpawn;

    [SerializeField] TextMeshProUGUI p1TF;
    [SerializeField] TextMeshProUGUI p2TF;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public delegate void PlayerServe(Player player);
    public static event PlayerServe playerServe;

    public delegate void PlayerWonSet(Player player);
    public static event PlayerWonSet playerWonMatch;

    public delegate void PhaseStart();
    public static event PhaseStart phaseStart;

    public delegate void GameStart();
    public static event GameStart gameStart;

    private void OnEnable()
    {
        ScoreManager.playerWonGame += WinGame;
        ScoreManager.playerScored += StartPhase;
    }

    private void OnDisable()
    {
        ScoreManager.playerWonGame -= WinGame;
        ScoreManager.playerScored -= StartPhase;
    }
    void Start()
    {
        InitSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSet(Player player)
    {

    }

	void InitSet()
	{
		phaseStart?.Invoke();
		ServePhase();
	}

	void StartPhase(Player player)
    {
        phaseStart?.Invoke();
        SwapServer();
        ServePhase();
    }

    void SwapServer()
    {
        switch(server)
        {
            case Player.PLAYER_ONE:
                server = Player.PLAYER_TWO; 
                break;
            case Player.PLAYER_TWO:
                server = Player.PLAYER_ONE;
                break; 
        }
    }

    void ServePhase()
    {
        GameObject parent = p1BallSpawn;
        Player controller = Player.PLAYER_ONE;
        switch(server)
        {
            case Player.PLAYER_ONE:
                parent = p1BallSpawn;
                controller = Player.PLAYER_ONE;
                break;

            case Player.PLAYER_TWO:
                parent = p2BallSpawn;
                controller = Player.PLAYER_TWO;
                break;
        }
        GameObject ball = Instantiate(ballPF, parent.transform.position, Quaternion.identity);
        ball.transform.parent = parent.transform;
        ball.GetComponent<Ball>().setController(controller);
        ///Debug.Log("Spawning at " + ballSpawn);
    }

	#region WINS

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

    #endregion
}
