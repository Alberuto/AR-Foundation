using UnityEngine;

public class EnemyMover : MonoBehaviour {

    public float speed = 3f;

    void Update() {

        transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);

    }
}