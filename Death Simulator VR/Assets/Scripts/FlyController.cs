using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour {
    [SerializeField]
    Rigidbody rb;
    
    public float speed;
    public float amplitude;

    public float x;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        x = Random.Range(-7, 7);
        Vector3 movement = new Vector3(x, Mathf.Sin(x) * amplitude, 0);
        rb.velocity = movement * speed;
    }
}
