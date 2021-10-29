using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(4.2f,-4f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4.5f)
            Destroy(gameObject);
    }
}
