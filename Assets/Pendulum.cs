using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    // Tốc độ di chuyển qua lại của đối tượng
    public float speed = 1.0f;

    // Biên độ của chuyển động (khoảng cách tối đa đối tượng sẽ di chuyển từ vị trí ban đầu)
    public float amplitude = 1.0f;

    // Vị trí ban đầu của đối tượng
    private Vector3 startPosition;

    private void Start()
    {
        // Lưu lại vị trí ban đầu của đối tượng
        startPosition = transform.position;
    }

    private void Update()
    {
        // Tính toán vị trí mới của đối tượng
        float x = startPosition.x + Mathf.Sin(Time.time * speed) * amplitude;

        // Cập nhật vị trí của đối tượng
        transform.position = new Vector3(x, startPosition.y, startPosition.z);
    }
}
