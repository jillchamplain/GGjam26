using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerHit : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3[] racketPoses;
    [SerializeField] CapsuleCollider collider;
    [SerializeField] float swingSpeed;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void SwapRacket(Player player, char direction)
    {
        if (this.player != player)
            return;
        switch(direction)
        {
            case 'u':
            case 'd':
                this.transform.position = racketPoses[1];
                break;
            case 'l':
                this.transform.position = racketPoses[0];
                break;
            case 'r':
                this.transform.position = racketPoses[2];
                break;
        }
    }
}
