using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnSphere : MonoBehaviour
{
    public Transform planetCenter;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    public float surfaceOffset = 1.0f; // Abstand von Kugelmitte zur Oberfläche + Spielerhöhe/Radius

    void Update()
    {
        // Richtung von der Kugelmitte zum Spieler (quasi die "oben"-Richtung für den Spieler)
        Vector3 gravityUp = (transform.position - planetCenter.position).normalized;

        // Spieler so ausrichten, dass "oben" = normal zur Kugeloberfläche
        transform.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        // Eingabe (WASD oder Pfeiltasten)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Bewegungsrichtung entlang der Oberfläche (Tangentialebene)
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        moveDirection = Vector3.ProjectOnPlane(moveDirection, gravityUp).normalized;

        // Bewegung ausführen
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Nach der Bewegung: Korrigiere die Position auf die Oberfläche der Kugel
        gravityUp = (transform.position - planetCenter.position).normalized;
        //transform.position = planetCenter.position + gravityUp * (planetCenter.localScale.x * 0.5f + surfaceOffset);
        transform.position = planetCenter.position + gravityUp * (5 + surfaceOffset);
        // Spieler in Blickrichtung drehen
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, gravityUp);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
