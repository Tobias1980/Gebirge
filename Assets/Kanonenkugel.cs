using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kanonenkugel : MonoBehaviour
{
    public float speed=0;
    public Vector3 direction;

    public Kanonenkugel(float speed, Vector3 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    public Kanonenkugel(Vector3 direction)
    {
        this.direction = direction;
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; 
    }

}
