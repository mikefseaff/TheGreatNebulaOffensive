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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0);
       // StartCoroutine("ShotTimer");
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
        for (int i = 0; i < 4; i++)
        {
            GameObject specialBullet = Instantiate(bullet, new Vector3(transform.position.x + .1f, transform.position.y), transform.rotation);
            specialBullet.GetComponent<bullet_controller>().speedX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
            specialBullet.GetComponent<bullet_controller>().speedY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
            //Debug.Log(specialBullet.GetComponent<Rigidbody2D>().velocity);
        }
    }

    IEnumerator ShotTimer()
    {

        while (timer <= shootingTimer)
        {
            
            //Debug.Log("less than");
            if (timer >= shootingTimer)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject specialBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y),transform.rotation);
                    specialBullet.GetComponent<bullet_controller>().speedX = 7 * Mathf.Cos(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
                    specialBullet.GetComponent<bullet_controller>().speedY = 7 * Mathf.Sin(Mathf.Deg2Rad * (transform.localEulerAngles.z + (i * 90)));
                    //Debug.Log(specialBullet.GetComponent<Rigidbody2D>().velocity);
                }
                
                

                //figure out this stupidity michael it doesnt change the velocity :sadge:


                timer = 0;

            }
            timer += .001f;
            yield return new WaitForSecondsRealtime(.001f);
        }

    }
}
