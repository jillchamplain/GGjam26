using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] MoveControls controls;

    [Serializable]
    public struct MoveControls
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode leftKey;
        public KeyCode rightKey;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(controls.upKey))
        {
            rb.AddForce(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }

        if(Input.GetKeyDown(controls.downKey))
        {
			rb.AddForce(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
		}

        if(Input.GetKeyDown(controls.leftKey))
        {
			rb.AddForce(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
		}

        if(Input.GetKeyDown(controls.rightKey))
        {
			rb.AddForce(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
		}
    }
}
