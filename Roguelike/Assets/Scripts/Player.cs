using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject friendlyBullet;
  //public GameObject friendlyLaser;
  public float fireDelay;
  float fireTime = 0;

  float chargeTime = 0;
  Vector2 lastAim;
  float health = 100;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Move(Touch touch)
  {
    Vector2 pos, start;
    start = transform.position;
    pos = Camera.main.ScreenToWorldPoint(touch.position);
    if(Vector2.Distance(start, pos) < 50) //wasn't a slide
    {
      transform.position = new Vector2(pos.x, pos.y);
    }
    //transform.position = Vector2.MoveTowards(start, pos, speed * Time.deltaTime);
  }
  void Shoot(Vector2 aim, float speed, bool fullyCharged)
  {
    Vector2 dir = aim - (new Vector2(transform.position.x, transform.position.y));
    dir.Normalize();
    GameObject bullet = Instantiate (friendlyBullet, transform.position, Quaternion.identity) as GameObject;
    bullet.GetComponent<Rigidbody2D>().velocity = dir * speed;
    FriendlyBullet b = bullet.GetComponent<FriendlyBullet>();
    b.setSpeed(speed);
    if(fullyCharged)
    {
      bullet.GetComponent<CircleCollider2D>().isTrigger = true;
    }
  }
  /*
  void Laser(Vector2 aim, float speed)
  {
    Vector2 dir = aim - (new Vector2(transform.position.x, transform.position.y));
    dir.Normalize();
    GameObject laser = Instantiate (friendlyLaser, transform.position, Quaternion.identity) as GameObject;
    laser.GetComponent<Rigidbody2D>().velocity = dir * speed;
    FriendlyBullet b = bullet.GetComponent<FriendlyLaser>();
    b.setSpeed(speed);
  }
  */
  void Update()
  {
    Touch[] touch = Input.touches;
    if(Input.touchCount == 1)
    {
      Move(touch[0]);
      if(chargeTime > 0 && Time.time > fireTime)
      {
        //Debug.Log(chargeTime);
        fireTime = Time.time + fireDelay;
        Shoot(lastAim, (1 + chargeTime) * 150, false);
        chargeTime = 0;
      }
    }
    else if(Input.touchCount == 2)
    {
      Move(touch[0]);
      chargeTime += touch[1].deltaTime;
      lastAim = Camera.main.ScreenToWorldPoint(touch[1].position);
      if(chargeTime >= 1)
      {
        Shoot(lastAim, 500, true);
        chargeTime = 0;
      }
    }
  }
  void OnCollisionEnter2D (Collision2D col)
  {
    if(col.gameObject.tag.Equals("Enemy"))
    {
      health -= 10;
      Debug.Log(health);
      Destroy(col.gameObject);
    }
  }
}
