using UnityEngine;

public class PlayerOnSphereLatLong : MonoBehaviour
{
    [Header("Kugelreferenz")]
    public Transform planetCenter;     // Zentrum der Kugel
    public float sphereRadius = 5f;    // Radius der Kugel
    public float surfaceOffset = 1f;   // Abstand zur Oberfläche (z. B. halbe Höhe der Capsule)
    public Rigidbody rb;
    [Header("Bewegung")]
    public float latSpeed = 30f;       // Winkelgeschwindigkeit Breitengrad (W/S)
    public float lonSpeed = 30f;       // Winkelgeschwindigkeit Längengrad (A/D)
    public float jumpForce = 5f;
    private float latitude = 0f;       // -90 (Südpol) bis 90 (Nordpol)
    private float longitude = 0f;      // 0 bis 360
    private float gravityStärke = 5f;
    
    
    void FixedUpdate()
    {
        Vector3 gravitation = (planetCenter.position - transform.position).normalized * gravityStärke;
        if (!IsGrounded())
        {
            rb.AddForce(gravitation);
        }
        
    }


    void Update()
    {
        // Eingabe
        float latInput = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        float lonInput = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;

        // Winkel aktualisieren
        if (latInput!=0 || lonInput!=0)
        {
            latitude += latInput * latSpeed * Time.deltaTime;
            longitude += lonInput * lonSpeed * Time.deltaTime;

            // Breitengrad begrenzen (nicht über die Pole hinaus)
            latitude = Mathf.Clamp(latitude, -89f, 89f);  // Nie genau -90/+90, sonst Matheprobleme

            // In Radiant umrechnen
            float latRad = latitude * Mathf.Deg2Rad;
            float lonRad = longitude * Mathf.Deg2Rad;

            // Position auf der Kugel berechnen
            float r = sphereRadius + surfaceOffset;
            float x = r * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
            float y = r * Mathf.Sin(latRad);
            float z = r * Mathf.Cos(latRad) * Mathf.Sin(lonRad);

            Vector3 newPos = new Vector3(x, y, z) + planetCenter.position;
            transform.position = newPos;

            // Figur ausrichten: "Up" zeigt weg vom Zentrum
            Vector3 gravityUp = (transform.position - planetCenter.position).normalized;

            // Blickrichtung: Tangente entlang der Breite (zur Vereinfachung)
            Vector3 forward = Vector3.Cross(Vector3.up, gravityUp);
            if (forward.sqrMagnitude < 0.01f) // in Polnähe: Cross-Produkt unzuverlässig
                forward = Vector3.Cross(Vector3.right, gravityUp);

            transform.rotation = Quaternion.LookRotation(forward, gravityUp);
        }
        


    
    if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Vector3 jumpDirection = (transform.position - planetCenter.position).normalized;
            rb.AddForce(jumpDirection * jumpForce);//, ForceMode.Impulse);
            Debug.Log($"Sprungrichtung: {jumpDirection.ToString()}  Kraft: {jumpForce.ToString() }");
        }
    }

    bool IsGrounded()
    {
        Vector3 richtungZurKugel = (planetCenter.position - transform.position).normalized;
        float abstandZurKugel = 1.1f;
        return Physics.Raycast(transform.position, richtungZurKugel, abstandZurKugel);
    }
}
