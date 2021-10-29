using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public GameObject character;
    public GameObject deco;
    float speed = 2f;
    float delta = 0.15f;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = character.transform.position;
        StartCoroutine(shootingStar());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 p = pos;
        p.y += delta * Mathf.Sin(Time.time * speed);
        character.transform.position = p;
    }

    IEnumerator shootingStar()
    {
        while(true)
        {
            GameObject deco1 = Instantiate(deco, new Vector3(Random.Range(-4f,1f), Random.Range(3f,6f), transform.position.z), Quaternion.Euler(0,0,40));
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            GameObject deco2 = Instantiate(deco, new Vector3(Random.Range(-4f,1f), Random.Range(3f,6f), transform.position.z), Quaternion.Euler(0,0,40));
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            
        }
    }
}
