using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Collider))]
public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float forceThrust = 1000f;
    [SerializeField]
    private Vector3 forceAngle = new Vector3(0f, 0.3f, 0f);
    public float ForceAngle{
        set{
            forceAngle = new Vector3(0f, value, 0f);
        }
    }
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private ParticleSystem blastEffect;
    private Vector3 fireDirection;

    private Transform bulletOwner;
    public Transform BulletOwner{
        set{
            bulletOwner = value;
        }
    }

    private int attackPower = 30;
    public int AttackPower{
        set{
            attackPower = value;
        }
    }

    private void Start()
    {
        bullet = GetComponent<Rigidbody>();
    }

    public void Fire(){
        fireDirection = bulletOwner.transform.forward;
        Vector3 force = (fireDirection + forceAngle)*forceThrust;
        bullet.AddForce(force, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TankController>() == null && other.GetComponent<Idamagable>() != null ){
            Idamagable hit = other.GetComponent<Idamagable>();
            hit.Damage(attackPower);
            bullet.velocity = Vector3.zero;
            bullet.angularVelocity = Vector3.zero;
            bullet.GetComponent<Collider>().enabled = false;
            StartCoroutine(blastBullet(blastEffect));
        } else if(other.GetComponent<TankController>() == null){
            bullet.GetComponent<Collider>().enabled = false;
            bullet.velocity = Vector3.zero;
            bullet.angularVelocity = Vector3.zero;
            StartCoroutine(blastBullet(blastEffect));
        }
    }

    IEnumerator blastBullet(ParticleSystem blastEffect){
        blastEffect.Play();
        float playBackTime = blastEffect.main.duration;
        Debug.Log("PB time : " + playBackTime);
        yield return new WaitForSecondsRealtime(playBackTime);
        Destroy(this.gameObject);
    }
}