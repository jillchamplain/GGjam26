using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    static Ball inst;
    bool isBallServed = false;
    [SerializeField] int numBounces;
    [SerializeField] Player curController;
    public void setController(Player controller) {  curController = controller; }

    [SerializeField] Vector3 serveForce;
    [SerializeField] float servePower;
    [SerializeField] float serveTime;
    [SerializeField] AnimationCurve ballBounce;
    [SerializeField] Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void BallBouncedTwice(Player player); //player courtside
    public static event BallBouncedTwice ballBouncedTwice;

    public delegate void BallServed(Player player);
    public static event BallServed ballServed;

    private void OnEnable()
    {
        Courtside.ballHitCourt += BounceBall;    }

    private void OnDisable()
    {
		Courtside.ballHitCourt -= BounceBall;
	}
    private void Start()
    {
        if (!inst)
            inst = this;
        else if(inst)
        {
           Destroy(inst.gameObject);
            inst = this; 
        }
        TogglePhysics(false);
        StartCoroutine(Serve());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ServeHit(); 
        }
    }

    #region ACTIVE BALL STATE

    void BounceBall(Player player) //
    {
		numBounces++;
		if (numBounces == 2)
		{
            if(player == Player.PLAYER_ONE)
            {
                ballBouncedTwice?.Invoke(Player.PLAYER_TWO);
                return;
            }
            if(player == Player.PLAYER_TWO)
            {
                ballBouncedTwice?.Invoke(Player.PLAYER_ONE);
            }

		}
	}

    void SwapBallController()
    {
        switch(curController)
        {
            case Player.PLAYER_ONE:
                curController = Player.PLAYER_TWO;
                break;
            case Player.PLAYER_TWO:
                curController = Player.PLAYER_ONE;
                break;
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

    IEnumerator Serve()
    {
        transform.DOLocalJump(transform.localPosition, servePower, 1, serveTime).SetEase(ballBounce);
        yield return new WaitForSeconds(serveTime);
        if (!isBallServed)
            StartCoroutine(Serve());

	}

    public void ServeHit()
    {
        if (isBallServed)
            return;
        isBallServed = true;
		TogglePhysics(true);
		transform.DOKill();
        //Debug.Log("killing tweens");
        switch(curController)
        {
            case Player.PLAYER_ONE:
                Vector3 p1ServeForce = new Vector3(-serveForce.x, serveForce.y, serveForce.z);
				//Debug.Log("using 1 " + p1ServeForce);
				rb.AddRelativeForce(p1ServeForce, ForceMode.Force);
				break;
            case Player.PLAYER_TWO:
                Vector3 p2ServeForce = new  Vector3(serveForce.x, serveForce.y, serveForce.z);
				rb.AddRelativeForce(p2ServeForce, ForceMode.Force);
				//Debug.Log("using " + p2ServeForce);
				break;
                
        }
        ballServed?.Invoke(curController);
	}
}
