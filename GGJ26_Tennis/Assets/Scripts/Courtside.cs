using UnityEngine;

public class Courtside : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Player player;

    public delegate void BallHitCourt(Player player);
    public static event BallHitCourt ballHitCourt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Ball>())
        {
            ballHitCourt.Invoke(player);
        }
    }
}
