using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildGeneratorController : MonoBehaviour
{
    public int phaseNumber;
    public delegate void SheildGenerator();
    public static event SheildGenerator Destroyed;
    public int health;
    private float timer;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyedEvent()
    {
        if (Destroyed != null)
            Destroyed();
    }

    private void OnDisable()
    {
        DestroyedEvent();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bullet_player")
        {
            health -= 1;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f, 1);
            StartCoroutine("HitDelay");

        }
        if (health == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.localScale.x * 4.5f, this.transform.localScale.y * 4.5f);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);

        }

    }

    IEnumerator Die(GameObject Enemy)
    {
        while (true)
        {

            if (Enemy.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                Debug.Log("hi");
                if (timer >= .8f)
                {
                    Debug.Log("bye");
                    GameObject.Destroy(Enemy);
                }
                timer += .8f;
                yield return new WaitForSeconds(.8f);
            }
            yield return null;

        }



    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(.2f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
}
