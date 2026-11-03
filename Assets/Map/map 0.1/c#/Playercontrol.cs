using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playcontrol : MonoBehaviour
{
    public float runSpeed;
    public float JumpSpeed;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private bool isGround;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Jump();
        CheckGround();

    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("run", playHasXAxisSpeed);
    }
    void Flip()
    {
        bool playHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (myRigidbody.velocity.x > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (myRigidbody.velocity.x < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVel = new Vector2(0.0f, JumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
            bool playHasYAxisSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnim.SetBool("Jump", playHasYAxisSpeed);
        }
    }
}



