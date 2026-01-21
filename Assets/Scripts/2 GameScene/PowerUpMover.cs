using UnityEngine;

public class PowerUpMover : MonoBehaviour {

    public static float speed = 3f;

    void Update() {

        transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);

    }
}