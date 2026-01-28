using UnityEngine;

public class EnemyMover : MonoBehaviour {
    public static float speed = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Falta Rigidbody en " + name);
    }

    void FixedUpdate()
    {  //FixedUpdate para físicas
        Vector3 move = Vector3.forward * -speed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}