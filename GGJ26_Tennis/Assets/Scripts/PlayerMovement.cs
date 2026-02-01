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
            isServing = !isServing;
        }
        else
            isServing = false;
    }

    void ResetPosition()
    {
        Debug.Log("reset");
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
        if(Input.GetKey(controls.upKey) && isServing!)
        {
            if(player == Player.PLAYER_ONE)
                rb.AddForce(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            else
                rb.AddForce(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }

        if(Input.GetKey(controls.downKey) && !isServing)
        {
            if(player == Player.PLAYER_ONE)
                rb.AddForce(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            else
                rb.AddForce(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }

        if(Input.GetKey(controls.leftKey))
        {
            if(player == Player.PLAYER_ONE)
			    rb.AddForce(new Vector3(0,0, -moveSpeed * Time.deltaTime));
            else
                rb.AddForce(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }

        if(Input.GetKey(controls.rightKey))
        {
            if(player == Player.PLAYER_ONE)
			    rb.AddForce(new Vector3(0, 0, moveSpeed * Time.deltaTime));
            else
                rb.AddForce(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }
    }
}
