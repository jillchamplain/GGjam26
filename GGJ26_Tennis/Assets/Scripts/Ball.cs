using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int numBounces;
    [SerializeField] Player curController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void BallBouncedTwice(Player player); //player courtside
    public static event BallBouncedTwice ballBouncedTwice;

    private void OnEnable()
    {
        Courtside.ballHitCourt += BounceBall;
    }

    private void OnDisable()
    {
		Courtside.ballHitCourt -= BounceBall;
	}

    void BounceBall(Player player)
    {
        Debug.Log("bounce");
		numBounces++;
		if (numBounces == 2)
		{
			ballBouncedTwice?.Invoke(player);
            
		}
	}

    void ChangeBallController(Player player)
    {
        numBounces = 0;
        curController = player;
    }
}
