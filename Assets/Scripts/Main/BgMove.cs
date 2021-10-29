using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    float speed = 1.5f;
    float delta = 0.15f;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 p = pos;
        p.x += delta * Mathf.Cos(Time.time * speed);
        p.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = p;
    }
}
