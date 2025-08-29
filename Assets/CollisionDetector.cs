using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        string collidedName = collision.gameObject.name;
        Debug.Log($"{gameObject.name} kollidiert mit {collidedName}");
        if (collidedName=="Abgrund")
        {
            Destroy(gameObject);
        }



    }
}
