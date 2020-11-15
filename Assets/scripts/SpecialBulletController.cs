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
        StartCoroutine("ShotTimer");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 45 * Time.deltaTime);
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void SpecialShoot()
    {
            StartCoroutine("ShotTimer");
    }

    IEnumerator ShotTimer()
    {

        while (timer <= shootingTimer)
        {
            
            Debug.Log("less than");
            if (timer >= shootingTimer)
            {
                
                specialBullet = GameObject.Instantiate(bullet, new Vector3(transform.position.x,transform.position.y + GetComponent<Renderer>().bounds.size.y/2), Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
               // specialBullet.GetComponent<Rigidbody2D>().velocity = specialBullet.transform.forward * specialBullet.GetComponent<bullet_controller>().speed;
                specialBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                Debug.Log(specialBullet.GetComponent<Rigidbody2D>().velocity);

                //figure out this stupidity michael it doesnt change the velocity :sadge:
                // GameObject.Instantiate(bullet, transform.position, transform.rotation);
                //GameObject.Instantiate(bullet, transform.position, transform.rotation);
                //GameObject.Instantiate(bullet, transform.position, transform.rotation);


                timer = 0;
                //yield break;

            }
            timer += .001f;
            yield return new WaitForSecondsRealtime(.001f);
        }

    }
}
