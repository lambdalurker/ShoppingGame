using UnityEngine;

public class CartFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(1.5f, 0f, 0f);
    public float followSpeed = 5f;
    public float groundY = 0.5f; // height above ground

    void Update()
    {
        Vector3 targetPos = player.position + player.TransformDirection(offset);
        targetPos.y = groundY; // always keep cart at this height, ignore player Y
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        // Only rotate on Y axis (no tilting)
        float targetAngle = player.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.Euler(0, targetAngle, 0), followSpeed * Time.deltaTime);
    }
}