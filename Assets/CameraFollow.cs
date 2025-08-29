using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Der Spieler
    public float distanceBehind = 4f;  // Abstand hinter dem Spieler
    public float heightAbove = 2f;     // Höhe über dem Spieler
    public float smoothSpeed = 5f;     // Glättung

    void LateUpdate()
    {
        if (target == null) return;

        // Richtung von Spieler nach oben (senkrecht zur Kugel)
        Vector3 up = target.up;

        // Blickrichtung des Spielers (vorwärts entlang der Kugeloberfläche)
        Vector3 back = -target.forward;

        // Zielposition der Kamera (leicht versetzt hinter und über dem Spieler)
        Vector3 targetPosition = target.position + back * distanceBehind + up * heightAbove;

        // Glatte Bewegung
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Kamera schaut immer zum Spieler
        transform.LookAt(target.position, up);
    }
}
