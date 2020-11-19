using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Collider))]
public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float forceThrust = 2f;
    [SerializeField]
    private Vector3 forceAngle;
    [SerializeField]
    private Rigidbody bullet;

    private Vector3 fireDirection;

    private Vector3 position;
    private Vector3 forward;
    private void Awake()
    {
        position = transform.position;
        forward = transform.forward;
    }
    private void Fire()
    {
        bullet = GetComponent<Rigidbody>();
        fireDirection = transform.forward;
        Vector3 force = (fireDirection + forceAngle)*forceThrust;
        bullet.AddForce(force, ForceMode.Force);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Fire();
        }
    }
}
