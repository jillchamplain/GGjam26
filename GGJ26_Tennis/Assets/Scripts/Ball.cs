using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int numBounces;
    [SerializeField] Player curController;

    [SerializeField] Vector3 serveForce;
    [SerializeField] float servePower;
    [SerializeField] float serveTime;

    [SerializeField] Rigidbody rb;

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
    private void Start()
    {
        TogglePhysics(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ServeHit(); 
        }
    }

    #region ACTIVE BALL STATE

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

    #endregion

    public void TogglePhysics(bool  ball)
    {
        rb.useGravity = ball;
    }

    public void Serve()
    {
        transform.DOJump(transform.position, servePower, 1, serveTime);
    }

    public void ServeHit()
    {
		TogglePhysics(true);
		transform.DOKill();
        switch(curController)
        {
            case Player.PLAYER_ONE:
				rb.AddRelativeForce(serveForce, ForceMode.Force);
				break;
            case Player.PLAYER_TWO:
				rb.AddRelativeForce(serveForce, ForceMode.Force);
				break;
                
        }
	}
}
