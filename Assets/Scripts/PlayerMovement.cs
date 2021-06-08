using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer Luigi;

    public float speed;
    public bool isCrouched;
    public bool isUp;
    public bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Luigi = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        anim.SetBool("isCrouched", isCrouched);
        anim.SetBool("isUp", isUp);
        anim.SetBool("isFrozen", isFrozen);

        if (!Luigi.flipX && horizontalInput > 0 || Luigi.flipX && horizontalInput < 0)
        {
            Luigi.flipX = !Luigi.flipX;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            isCrouched = true;
            isFrozen = true;
        }
        else
        {
            isCrouched = false;
            isFrozen = false;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            isFrozen = true;
            isUp = true;
        }
        else
        {
            isUp = false;
            isFrozen = false;
        }

        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));

        if (isFrozen)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
