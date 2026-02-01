using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
public class PlayerHit : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3[] racketPoses;
    int racketPosIndex;
    [SerializeField] CapsuleCollider collider;
    [SerializeField] float swingSpeed;
    bool canSwing = true;
    [SerializeField] KeyCode hitKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        PlayerMovement.playerUp += SwapRacket;
        PlayerMovement.playerDown += SwapRacket;

        PlayerMovement.playerLeft += SwapRacket;
        PlayerMovement.playerRight += SwapRacket;
    }

    private void OnDisable()
    {
        PlayerMovement.playerUp -= SwapRacket;
        PlayerMovement.playerDown -= SwapRacket;

        PlayerMovement.playerLeft -= SwapRacket;
        PlayerMovement.playerRight -= SwapRacket;
    }
    void Start()
    {
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hitKey))
            StartCoroutine(Swing());
    }

    void ServeSwing()
    {
        
    }

    IEnumerator Swing()
    {
        Debug.Log("Swing");
        if (canSwing)
        {
            canSwing = false;
            collider.enabled = true;
            yield return new WaitForSeconds(swingSpeed);
            collider.enabled = false;
            canSwing = true;
        }

    }

    void SwapRacket(Player player, char direction)
    {
        if (this.player != player)
            return;
        switch(direction)
        {
            case 'u':
                this.transform.localPosition = racketPoses[2];
                racketPosIndex = 1;
                break;
            case 'd':
                this.transform.localPosition = racketPoses[2];
                racketPosIndex = 1;
                break;
            case 'l':
                this.transform.localPosition = racketPoses[0];
                racketPosIndex = 0;
                break;
            case 'r':
                this.transform.localPosition = racketPoses[2];
                racketPosIndex = 2;
                break;
        }
    }

    void ReturnSwing(GameObject ball, Vector3 lVelocity)
    {
        //ball.GetComponent<Rigidbody>().angularVelocity = -aVelocity;
        ball.GetComponent<Rigidbody>().AddForce(-lVelocity, ForceMode.Impulse);
        Debug.Log("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Ball>())
        {
            ReturnSwing(other.gameObject, other.gameObject.GetComponent<Rigidbody>().linearVelocity);
        }
    }
}
