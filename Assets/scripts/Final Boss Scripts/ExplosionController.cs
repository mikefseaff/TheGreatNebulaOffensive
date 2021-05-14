using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

        BridgeController.BridgeDestroyed += Explode;
        

    }

    private void OnDisable()
    {

        BridgeController.BridgeDestroyed -= Explode;

    }

    private void Explode()
    {
        StartCoroutine("RandomExplosions");
    }

    IEnumerator RandomExplosions()
    {
        while (true)
        {
            GameObject boom = GameObject.Instantiate(explosion, new Vector3(this.transform.position.x * Random.Range(-1.5f,1.5f), this.transform.position.y * Random.Range(-1.5f, 1.5f), 0), new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.root.transform.localScale.x * Random.Range(-2f,2f), this.transform.root.transform.localScale.y * Random.Range(-2f,2f));
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
            yield return new WaitForSeconds(Random.Range(.2f,.7f));
        }
        
    }
}
