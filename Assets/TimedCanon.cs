using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimedCanon : MonoBehaviour
{
    //[SerializeField] private int gesamteKK=30;
    //[SerializeField] private float shotfrequency;
    //[SerializeField] private Kanonenkugel kanonenkugelPrefab;
    //[SerializeField] private float speed=0;
    //// Start is called before the first frame update
    //private IEnumerator Start()
    //{
    //    int i = 0;
    //    while (i<gesamteKK)
    //    {
    //        i = i+1;
    //        ShootBall();
    //        yield return new WaitForSeconds(shotfrequency);
    //    }
    //}

    //// Update is called once per frame
    //public float rotationSpeed = 100f; // Rotationsgeschwindigkeit

    //void Update()
    //{
    //    // Eingaben für horizontale und vertikale Rotation
    //    float rotateX = Input.GetAxis("Vertical");   // W/S oder Pfeiltasten hoch/runter
    //    float rotateY = Input.GetAxis("Horizontal"); // A/D oder Pfeiltasten links/rechts

    //    // Berechne die Rotation basierend auf den Eingaben
    //    float xRotation = rotateX * rotationSpeed * Time.deltaTime;
    //    float yRotation = rotateY * rotationSpeed * Time.deltaTime;

    //    // Anwenden der Rotation auf das GameObject
    //    transform.Rotate(xRotation, yRotation, 0);
    //}

    //private void ShootBall()
    //{
    //    Kanonenkugel newkanonenkugel = Instantiate(kanonenkugelPrefab,transform.position,Quaternion.identity);
    //    newkanonenkugel.direction = transform.forward;
    //    newkanonenkugel.speed = speed;

    //    Destroy(newkanonenkugel.gameObject,3f);

    //}

    
        public GameObject cannonballPrefab; // Das Prefab der Kanonenkugel
        public Transform cannonMuzzle;      // Der Ausgangspunkt für die Kanonenkugel (z.B. das Ende des Kanonenrohrs)
        public float fireForce = 10f;       // Die Schusskraft der Kanonenkugel
        public float rotationSpeed = 100f; // Rotationsgeschwindigkeit
   
    void Update()
        {
            // Prüfen, ob die Leertaste gedrückt wurde
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireCannonball();
            }

        //Eingaben für horizontale und vertikale Rotation
            float rotateX = Input.GetAxis("Vertical");   // W/S oder Pfeiltasten hoch/runter
            float rotateY = Input.GetAxis("Horizontal"); // A/D oder Pfeiltasten links/rechts

            // Berechne die Rotation basierend auf den Eingaben
            float xRotation = rotateX * rotationSpeed * Time.deltaTime;
            float yRotation = rotateY * rotationSpeed * Time.deltaTime;

            // Anwenden der Rotation auf das GameObject
            transform.Rotate(xRotation, yRotation, 0);
    }

    void FireCannonball()
    {

        // Erstellen der Kanonenkugel an der Position des Mündungsrohrs
        //cannonMuzzle.position += cannonMuzzle.up;
        Vector3 positonStart = cannonMuzzle.position + cannonMuzzle.up;
        GameObject cannonball = Instantiate(cannonballPrefab, positonStart, cannonMuzzle.rotation);

        // Der Rigidbody der Kanonenkugel, um die Schusskraft anzuwenden
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Anwenden einer Kraft in Richtung des Mündungsrohrs
            rb.AddForce(cannonMuzzle.up * fireForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log($"Sorry rb == null");
        }
    }
 }


