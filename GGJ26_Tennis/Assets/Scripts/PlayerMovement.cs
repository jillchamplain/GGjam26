using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] MoveControls controls;

    [SerializeField] bool isServing;

    [SerializeField] Vector3 ogPos;

    [Serializable]
    public struct MoveControls
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode leftKey;
        public KeyCode rightKey;
    }

    public delegate void PlayerLeft(Player player, char direction);
    public static event PlayerLeft playerLeft;

    public delegate void PlayerRight(Player player, char direction);
    public static event PlayerRight playerRight;

    public delegate void PlayerUp(Player player, char direction);
    public static event PlayerUp playerUp;

    public delegate void PlayerDown(Player player, char direction);
    public static event PlayerDown playerDown;

    private void OnEnable()
    {
        SetGameManager.playerServe += ToggleServing;
        SetGameManager.phaseStart += ResetPosition;

        Ball.ballServed += ToggleServing;
    }

    private void OnDisable()
    {
        SetGameManager.playerServe -= ToggleServing;
        SetGameManager.phaseStart -= ResetPosition;

        Ball.ballServed -= ToggleServing;
    }

    void ToggleServing(Player player)
    {
        if (this.player == player)
        {
            Debug.Log(this.player + " was " + isServing);
            isServing = !isServing;
        }
    }

    void ResetPosition()
    {
        //Debug.Log("reset");
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        this.transform.position = ogPos;
    }

    private void Start()
    {
        ogPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(controls.upKey) && !isServing)
        {
            if(player == Player.PLAYER_ONE)
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            else
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            playerUp?.Invoke(player, 'u');
        }

        if(Input.GetKey(controls.downKey) && !isServing)
        {
            if(player == Player.PLAYER_ONE)
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            else
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(controls.leftKey))
        {
            if(player == Player.PLAYER_ONE)
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            else
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            playerLeft?.Invoke(player, 'l');
        }

        if(Input.GetKey(controls.rightKey))
        {
            if(player == Player.PLAYER_ONE)
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            else
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            playerRight?.Invoke(player, 'r');
        }
    }
}
