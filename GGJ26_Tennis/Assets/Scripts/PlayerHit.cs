using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    [SerializeField] BoxCollider collider;
    [SerializeField] float swingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        PlayerMovement.playerLeft += SwapRacket;
        PlayerMovement.playerRight += SwapRacket;
    }

    private void OnDisable()
    {
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

    void SwapRacket(Player player)
    {

    }
}
