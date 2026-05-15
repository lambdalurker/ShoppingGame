using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;
    public float openSpeed = 6f;
    public float openDistance = 1.5f;

    private Vector3 doorLeftClosed;
    private Vector3 doorRightClosed;
    private bool isOpen = false;

    void Start()
    {
        doorLeftClosed = doorLeft.position;
        doorRightClosed = doorRight.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isOpen = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isOpen = false;
    }

    void Update()
    {
        Vector3 leftTarget = isOpen ? doorLeftClosed + Vector3.left * openDistance : doorLeftClosed;
        Vector3 rightTarget = isOpen ? doorRightClosed + Vector3.right * openDistance : doorRightClosed;

        doorLeft.position = Vector3.Lerp(doorLeft.position, leftTarget, openSpeed * Time.deltaTime);
        doorRight.position = Vector3.Lerp(doorRight.position, rightTarget, openSpeed * Time.deltaTime);
    }
}