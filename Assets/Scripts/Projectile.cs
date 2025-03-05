using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed;
   private bool hit;
   private BoxCollider2D boxCollider;
   private Animator anim;
   private float direction;


   private void Awake()
   {
        anim =GetComponent<Animator>();
        boxCollider=GetComponent<BoxCollider2D>();
   }

   private void Update()
   {
        if (hit) return;
        float movementSpeed=speed*Time.deltaTime* direction;
        transform.Translate(movementSpeed,0,0);

   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
     hit=true;
     boxCollider.enabled=false;
     anim.SetTrigger("explode");

   }

   public void SetDirection( float _direction)
   {
    direction=_direction;
    gameObject.SetActive(true);
    hit=false;
    boxCollider.enabled=true;

    float localScaleX=transform.localScale.x;
    if (Mathf.Sign(localScaleX)!=_direction)
    {
        localScaleX=-localScaleX;
    }

    transform.localScale = new Vector2(localScaleX,transform.localScale.y);
    GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);

   }

   private void deactivate()
   {
    gameObject.SetActive(false);
   }
}
