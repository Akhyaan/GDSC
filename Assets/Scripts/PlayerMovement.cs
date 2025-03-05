using UnityEngine;

public class PlayerMovement: MonoBehaviour
{   [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    

    
    private void Awake()
    {   //Grab references for rigidbody to get animation
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {   
        horizontalInput=Input.GetAxis("Horizontal");
        
        

        //flipping code
        if(horizontalInput>0.01f)
        {
            transform.localScale=Vector3.one;
        }
        else if(horizontalInput<-0.01f)
        {
            transform.localScale=new Vector3(-1,1,1);
        }

        if (wallJumpCooldown>0.2f)
        {
                body.velocity= new Vector2(Input.GetAxis("Horizontal")*speed,body.velocity.y);
                if (onWall() && !isGrounded())
                {
                    body.gravityScale=0;
                    body.velocity= Vector2.zero;
                }
                else
                {
                    body.gravityScale=5 ;
                }
                
                if(Input.GetKey(KeyCode.Space))
                {
                    Jump();
                }
        }

        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //set animator
        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded",isGrounded());

        print(onWall());
    }

    private void Jump()
    {   
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x,jumpforce);
            anim.SetTrigger("jump");
        }
        else if(onWall()&& !isGrounded())
        {   
            if (horizontalInput==0)
            {
                body.velocity=new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale= new Vector2(-Mathf.Sign(transform.localScale.x),transform.localScale.y);
            }
            else
            {
                body.velocity=new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
            }
            wallJumpCooldown=0;
            
        }

    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, Vector2.down, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

     private bool onWall()
    {
        RaycastHit2D raycastHit= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, new Vector2(transform.localScale.x,0), 0.01f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
{
    return horizontalInput==0 && isGrounded() && !onWall();
}
}

