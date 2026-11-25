using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour
{
    [SerializeField] private float acceleration = 22f;
    [SerializeField] private float reverseAcceleration = 10f;
    [SerializeField] private float maxSpeed = 24f;
    [SerializeField] private float steering = 85f;
    [SerializeField] private float brakeStrength = 5f;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = new Vector3(0f, -0.45f, 0f);
    }

    private void FixedUpdate()
    {
        float throttle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");

        if (throttle > 0f && body.velocity.magnitude < maxSpeed)
            body.AddForce(transform.forward * throttle * acceleration, ForceMode.Acceleration);
        else if (throttle < 0f)
            body.AddForce(transform.forward * throttle * reverseAcceleration, ForceMode.Acceleration);

        float speedFactor = Mathf.Clamp01(body.velocity.magnitude / 3f);
        float direction = Vector3.Dot(body.velocity, transform.forward) >= 0f ? 1f : -1f;
        Quaternion turn = Quaternion.Euler(0f, steer * steering * speedFactor * direction * Time.fixedDeltaTime, 0f);
        body.MoveRotation(body.rotation * turn);

        if (Input.GetKey(KeyCode.Space))
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, brakeStrength * Time.fixedDeltaTime);
    }
}
