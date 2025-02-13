using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 10f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or Arrow Keys (default mapping)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Create a movement vector. Here we assume the camera moves along the X and Z axes.
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        // Apply movement. Multiplying by Time.deltaTime makes the movement frame rate independent.
        transform.position += movement * speed * Time.deltaTime;
    }

}
