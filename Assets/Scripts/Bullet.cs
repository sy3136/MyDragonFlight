using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Target;
    public float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoBulletMotion());
    }
    IEnumerator CoBulletMotion()
    {
        Target = GameObject.FindGameObjectWithTag("Monster");
        while (true)
        {
            if (Target == null) Destroy(this.gameObject);
            float theta = Mathf.Atan((Target.transform.position.y - transform.position.y)
                / (Target.transform.position.x - transform.position.x)) * 180f / Mathf.PI;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,0f,theta), 0.08f);
            transform.Translate(Vector2.right * speed);

            yield return null;
        }
    }
}
