using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    //public GameObject bullet;
    public GameObject specialAbility;
    public GameObject explosion;
    private float timer;

    public int currentSpecialCharge;
    public int maxSpecialCharge;

    public Text abilityChargeDisplay;

    public int deaths;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Die(this.gameObject));
        currentSpecialCharge = 0;
        Debug.Log(Time.timeScale);
        abilityChargeDisplay = abilityChargeDisplay.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();
        ShootSpecial();
        Shoot();
        UpdateText();

    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = PlayerBulletObjectPool.SharedInstance.GetPooledBullet();
            if (bullet != null)
            {
             
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bullet.GetComponent<bullet_controller>().speedX, bullet.GetComponent<bullet_controller>().speedY);
                TrackStats.SharedInstance.BulletsFired += 1;

            }
        }
    }

    void ShootSpecial()
    {
        if (((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && currentSpecialCharge >= maxSpecialCharge))
        {
            TrackStats.SharedInstance.TotalSpecialAbilitiesUsed += 1;
            GameObject.Instantiate(specialAbility, transform.position, transform.rotation);
            currentSpecialCharge = 0;
        }
    }

    void Move()
    {
       

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            float moveX = x * speed;
            float moveY = y * speed;

            rb.velocity = new Vector2(moveX, moveY);


    }

    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        Vector3 playerSize = GetComponent<Renderer>().bounds.size;

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
            Mathf.Clamp(this.transform.position.y, topBorder + playerSize.y / 2, bottomBorder - playerSize.y / 2),
            this.transform.position.z
        );


    }

    void UpdateText()
    {
        if (currentSpecialCharge <= 25)
        {
            abilityChargeDisplay.text = "Ability Charge: " + currentSpecialCharge * 4 + " %";
        }
    }
        
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
           
            GameObject.Destroy(collision.gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(collision.transform.localScale.x, this.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
        }
        if (collision.gameObject.layer == 12)
        {

            Destroy(this.gameObject);
        }


    }
    IEnumerator Die(GameObject player)
    {
        
        while (true)
        {

            if (player.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                Debug.Log("hi");
                if (timer >= .8f)
                {
                    Debug.Log("bye");
                    GameObject.Destroy(player);
                }
                timer += .8f;
                yield return new WaitForSeconds(.8f);
            }
            yield return null;

        }



    }
}
