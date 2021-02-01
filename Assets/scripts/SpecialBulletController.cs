using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float timer;
    public float shootingTimer;
    public GameObject bullet;
    public float rotationValue;
    private GameObject specialBullet;
    public GameObject explosion;

    public int numShots;
    public int numShotsMax;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0);
        StartCoroutine("SelfDestruct");
        InvokeRepeating("SpecialAbilityFire", 0f, .2f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 75 * Time.deltaTime);
        
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            Destroy(this.gameObject);
        }
    }


    void SpecialAbilityFire()
    {
        if (numShots <= numShotsMax)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject specialBullet = Instantiate(bullet, new Vector3(transform.position.x + .1f, transform.position.y), transform.rotation);
                specialBullet.GetComponent<bullet_controller>().speedX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
                specialBullet.GetComponent<bullet_controller>().speedY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
                //Debug.Log(specialBullet.GetComponent<Rigidbody2D>().velocity);
            }
            numShots += 1;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            //boom.transform.localScale = new Vector3(collision.transform.localScale.x, this.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
            Destroy(this.gameObject);
        }
       
    }

 


}
