using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class PlayerHit : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3[] racketPoses;
    int racketPosIndex;
    [SerializeField] CapsuleCollider collider;
    [SerializeField] float swingSpeed;
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
            Swing();
    }

    void ServeSwing()
    {
        
    }

    void Swing()
    {
        Debug.Log("swing");
        collider.enabled = true;

        //Sequence swingSequence = DOTween.Sequence();
        //swingSequence.Append(this.transform.DOLocalRotate(new Vector3(90, 0, 0), swingSpeed/2, RotateMode.Fast));
        //swingSequence.Join(this.transform.DOLocalMoveY(-0.3f, swingSpeed / 2));
        //swingSequence.Append(this.transform.DOLocalRotate(new Vector3(0, 0, -70), swingSpeed / 2, RotateMode.LocalAxisAdd));
        //swingSequence.Append(this.transform.DOLocalRotate(new Vector3(0, 0, 0), swingSpeed/2, RotateMode.Fast));
        switch (racketPosIndex)
        {
            case 0:
                break;

            case 2:
                break;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.GetComponent<Ball>())
            Debug.Log("Hit ball");
    }
}
