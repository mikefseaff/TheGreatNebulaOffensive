using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_controller2 : MonoBehaviour
{
    //Class used to control the second enemy type


    public float moveSpeed;

    //frequency of the enemies sin movement
    public float frequency = 20f;


    // magnitude of the enemies sin movement
    public float magnitude = 0.5f;


    public bool facingRight = true;

    public bool isLevel1 = false;


    public GameObject explosion;


    public Vector3 pos, localScale;

    private float timerBullet;
    private float maxTimerBullet;
    //public GameObject bullet;
    private GameObject player;

    public float timerMin = 5f;
    public float timerMax = 25f;
    public bool canfireBullets = true;
    public bool canMove = true;

   // public float bulletSpeedCheck;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

        localScale = transform.localScale;

        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            isLevel1 = true;
            magnitude = 0;
            canMove = false;
            canfireBullets = false;
            moveSpeed = 5f;

            
        }
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            isLevel1 = true;
            magnitude = .35f;
            canMove = true;
            canfireBullets = true;
            moveSpeed = 5f;
            this.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if (canfireBullets)
        {
            StartCoroutine("FireBullet");
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        if (canfireBullets)
        {
            StartCoroutine("FireBullet");
        }
        BossController.LeftOver += IsLeftOver;
    }

    private void OnDisable()
    {
        BossController.LeftOver -= IsLeftOver;
    }

    // Update is called once per frame
    void Update()
    {
       // bulletSpeedCheck = bullet.GetComponent<enemy_bullet_controller>().speedX;
       if ( canMove)
        {
            CheckWhereToFace();

            if (facingRight)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }

        }


        if (Camera.main.WorldToViewportPoint(transform.position).x < 0 && !isLevel1)
        {
            Destroy(this.gameObject);
        }
        if (player.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            StopAllCoroutines();
            enabled = false;
        }
    }

    // checks x location to see when to switch directional movement 
    void CheckWhereToFace()
    {


        if (Camera.main.WorldToViewportPoint(transform.position).x <= 0 && isLevel1)
        {
            facingRight = true;

        }


        else if (Camera.main.WorldToViewportPoint(transform.position).x >= 1)
        {
            facingRight = false;

        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;

        }

        transform.localScale = localScale;

    }

    // method to move the enemy from the left to the right 
    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    //method to move the enemy from the right to left
    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void SpawnBullet()
    {
        GameObject bullet = Enemy2BulletPool.SharedInstance.GetPooledBullet();
        
        if (bullet != null)
        {
            Vector3 spawnPoint = transform.position;
            //spawnPoint.y -= (bullet.GetComponent<Renderer>().bounds.size.y / 2) + (GetComponent<Renderer>().bounds.size.y / 2);
            if (facingRight)
            {
                spawnPoint.x += (bullet.GetComponent<Renderer>().bounds.size.x / 2) + (GetComponent<Renderer>().bounds.size.x / 2);
            }
            else if (!facingRight)
            {
                spawnPoint.x -= (bullet.GetComponent<Renderer>().bounds.size.x / 2) + (GetComponent<Renderer>().bounds.size.x / 2);
            }
            bullet.transform.position = spawnPoint;
            bullet.SetActive(true);
            if (facingRight)
                bullet.GetComponent<enemy_bullet_controller>().speedX = Mathf.Abs(bullet.GetComponent<enemy_bullet_controller>().speedX);
            else
                bullet.GetComponent<enemy_bullet_controller>().speedX = Mathf.Abs(bullet.GetComponent<enemy_bullet_controller>().speedX) * -1;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bullet.GetComponent<enemy_bullet_controller>().speedX, bullet.GetComponent<enemy_bullet_controller>().speedY);

        }

        
    }

    IEnumerator FireBullet()
    {
        while (canfireBullets)
        {
            if (timerBullet >= maxTimerBullet)
            {
                SpawnBullet();
                timerBullet = 0;
                maxTimerBullet = Random.Range(timerMin, timerMax);
            }

            timerBullet += .01f;
            yield return new WaitForSeconds(.01f);
        }

    }

    private void IsLeftOver()
    {
        TrackStats.SharedInstance.EnemiesDestroyed += 1;
        player.GetComponent<player_controller>().currentSpecialCharge += 1;



        gameObject.SetActive(false);
        
        GameObject boom = GameObject.Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        boom.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
        float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Destroy(boom.gameObject, animationTime);
    }
}