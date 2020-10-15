using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformerplayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator _anim;
    private BoxCollider2D _box;

    public float speed = 40f;
    public float jumpForce = 15f;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector2 movement = new Vector2(deltaX, rb.velocity.y);

        rb.velocity = movement;


        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        /*rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        */

        rb.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }

        rb.gravityScale = grounded && deltaX == 0 ? 0 : 1;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // hold space can jump higher than usual, but doesnt work well with my laptop keyboard. I suspect its because my keyboard has poor respond time, so right now 
        // I will use key Z for the extend jump instead of the space. ( now it serve as second jump) please change the keyCode.Z to Space when testing 
        // hopfully it can function as power jump.  In my laptop, it would only increase the jump force for every jump, but it suppose to have normal jump when press space
        // and power jump when hold space
        if ( Input.GetKey(KeyCode.Z)&& isJumping == true )
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Z)){
            isJumping = false;

        }

        movingplatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<movingplatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        //the following code break the game, stickman goes large when attach the movingplatform
        /*circular_movement platforms = null;
        if (hit != null)
        {
            platforms = hit.GetComponent<circular_movement>();
        }
        if (platforms != null)
        {
            transform.parent = platforms.transform;
        }
        else
        {
            transform.parent = null;
        }
        */

        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        //correct the player scale
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (deltaX != 0)
        {
            transform.localScale = new Vector3(
            Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }

    }


    //attempt for player move as circular platform move, but the following code not functioning. 
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("circular"))
        {
            this.transform.parent = col.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("circular"))
        {
            this.transform.parent = null;
        }
    }
    
}
