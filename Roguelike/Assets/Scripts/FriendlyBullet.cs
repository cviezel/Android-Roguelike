using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
  public Rigidbody2D rb;
  public float speed;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  // Update is called once per frame
  void Update()
  {
    if(this.name != "FriendlyBullet")
    {
      if(rb.velocity.x == 0)
      {
        Destroy(this.gameObject);
      }
      if(transform.position.x > 80 || transform.position.x > 80 || transform.position.y > 50 || transform.position.y < -50)
      {
        Destroy(this.gameObject);
      }
    }
  }
  public float getSpeed()
  {
    return speed;
  }
  public void setSpeed(float s)
  {
    speed = s;
  }
  void OnCollisionEnter2D (Collision2D col)
  {
    if(col.gameObject.tag.Equals("Wall"))
    {
      Destroy(this.gameObject);
    }
  }
}
