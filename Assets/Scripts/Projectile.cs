using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed;
  private bool hit;
  private BoxCollider2D boxCollider;
  private Animator anim;
  private float direction;
  private float lifetime;
  GameObject explosionAnim;


  private void Awake()
  {
    anim = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider2D>();
  }

  private void Update()
  {
    if (hit) return;
    float movementSpeed = speed * Time.deltaTime * direction;
    transform.Translate(movementSpeed, 0, 0);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    hit = true;
    boxCollider.enabled = false;
    anim.SetTrigger("explode");
    StartCoroutine(onCollision());
  }

  IEnumerator onCollision()
  {
    GameObject temp = Instantiate(explosionAnim, transform.position, transform.rotation);
    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

    yield return new WaitForSeconds(.5f);
    Destroy(temp);
    Destroy(this.gameObject);
  }

  public IEnumerator SetDirection(float _direction, GameObject explosion)
  {
    lifetime = 0;
    direction = _direction;
    gameObject.SetActive(true);
    hit = false;
    boxCollider.enabled = true;
    explosionAnim = explosion;

    float localScaleX = transform.localScale.x;
    if (Mathf.Sign(localScaleX) != _direction)
    {
      localScaleX = -localScaleX;
    }

    transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);

    yield return new WaitForSeconds(2);

    this.gameObject.SetActive(false);

  }

  private void deactivate()
  {
    gameObject.SetActive(false);
  }
}
