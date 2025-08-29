using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Der Spieler
    public float distanceBehind = 4f;  // Abstand hinter dem Spieler
    public float heightAbove = 2f;     // H�he �ber dem Spieler
    public float smoothSpeed = 5f;     // Gl�ttung

    void LateUpdate()
    {
        if (target == null) return;

        // Richtung von Spieler nach oben (senkrecht zur Kugel)
        Vector3 up = target.up;

        // Blickrichtung des Spielers (vorw�rts entlang der Kugeloberfl�che)
        Vector3 back = -target.forward;

        // Zielposition der Kamera (leicht versetzt hinter und �ber dem Spieler)
        Vector3 targetPosition = target.position + back * distanceBehind + up * heightAbove;

        // Glatte Bewegung
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Kamera schaut immer zum Spieler
        transform.LookAt(target.position, up);
    }
}
