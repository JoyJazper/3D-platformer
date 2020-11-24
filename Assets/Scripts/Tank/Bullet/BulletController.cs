using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(Collider))]
public class BulletController : MonoBehaviour
{
    private float forceThrust = 1000f;
    private Vector3 fireDirection;
    private Rigidbody bullet;

    [SerializeField]
    private ParticleSystem blastEffect;
    
    private Transform bulletOwner;
    public Transform BulletOwner{
        set{
            bulletOwner = value;
        }
    }
    private Vector3 forceAngle = new Vector3(0f, 0.3f, 0f);
    public float ForceAngle{
        set{
            forceAngle = new Vector3(0f, value, 0f);
        }
    }
    private int attackPower = 30;
    public int AttackPower{
        set{
            attackPower = value;
        }
    }

    private void Awake()
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
        if(other != bulletOwner.GetComponentInParent<Collider>() && other.GetComponent<IDamagable>() != null ){
            IDamagable hit = other.GetComponent<IDamagable>();
            hit.Damage(attackPower);
            KillBullet();
        } else if(other != bulletOwner.GetComponentInParent<Collider>()){
            KillBullet();
        }
    }

    private void KillBullet(){
        bullet.velocity = Vector3.zero;
        bullet.angularVelocity = Vector3.zero;
        bullet.GetComponent<Collider>().enabled = false;
        StartCoroutine(blastBullet(blastEffect));
    }

    IEnumerator blastBullet(ParticleSystem blastEffect){
        blastEffect.Play();
        float playBackTime = blastEffect.main.duration;
        yield return new WaitForSecondsRealtime(playBackTime);
        Destroy(this.gameObject);
    }
}