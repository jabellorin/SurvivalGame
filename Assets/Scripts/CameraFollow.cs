using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;

    private void Start() {
        speed = 3f;
    }

    private void FixedUpdate() {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }




}
