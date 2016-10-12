using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    private float rate = 1f;
    public float speed;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(speed, speed + 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Menu.running) return;

        Vector3 newPos = new Vector3(0f, 0f);
        newPos.x += ((Time.deltaTime / 20) * rate * speed);

        transform.position += newPos;

        if (transform.position.x > 3) Destroy(gameObject);
    }
}
