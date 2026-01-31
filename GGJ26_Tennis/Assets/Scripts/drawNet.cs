using UnityEngine;
using UnityEngine.InputSystem;

public class drawNet : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject red;
    [SerializeField] GameObject blu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray test = player1.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            //  Debug.DrawRay(test.origin, test.direction * 100, Color.green);
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = 10;
            GameObject dotty = Instantiate(red);
            dotty.transform.position = player1.GetComponent<Camera>().ScreenToWorldPoint(screenPosition);
            Destroy(dotty, 10);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Ray test = player1.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            //  Debug.DrawRay(test.origin, test.direction * 100, Color.green);
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = 10;
            GameObject dotty = Instantiate(blu);
            dotty.transform.position = player2.GetComponent<Camera>().ScreenToWorldPoint(screenPosition);
            Destroy(dotty, 10);

        }
    }


}
