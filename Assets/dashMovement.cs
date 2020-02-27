using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public GameObject dashEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
       if (direction == 0)
       {
           if (Input.GetKeyDown(KeyCode.LeftShift))
           {
               Instantiate(dashEffect, transform.position, Quaternion.identity);
               direction = 1;
           } else if(Input.GetKeyDown(KeyCode.RightShift)){
               Instantiate(dashEffect, transform.position, Quaternion.identity);
               direction = 2;
           } 
       } else
       {
           if (dashTime <= 0)
           {
               direction = 0;
               dashTime = startDashTime;
               rb.velocity = Vector2.zero;
           } else
           {
               dashTime -= Time.deltaTime;

               if (direction == 1)
               {
                   rb.velocity = Vector2.left * dashSpeed;
               } else if (direction == 2)
               {
                   rb.velocity = Vector2.right * dashSpeed;
               }
           }
       }
    }
}
