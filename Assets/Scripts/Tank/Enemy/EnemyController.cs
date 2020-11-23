using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour, Idamagable
{
    [SerializeField]
    private float speed = 0.8f;
    public float Speed{
        set{
            speed = value;
        }
    }

    [SerializeField]
    private float thrust = 0.5f;
    public float Thrust{
        set{
            thrust = value;
        }
    }

    [SerializeField]
    private int health = 100;
    public int Health{
        set{
            health = value;
        }
    }



    //private TankService tankService;
    //private Transform player;

    private void Start(){
        //tankService = TankService.Instance;
    }

    // private void LateUpdate(){
    //     if(player != null){
    //         transform.LookAt(player);
    //     } else {
    //         try{
    //             player = tankService.PlayerTransform;
    //         }
    //         catch{

    //         }
    //     }
    // }

    public void Damage(int damage){
        health = health - damage;
        if(health <= 0){
            StartCoroutine(blastTank());
        }
    }

    private IEnumerator blastTank(){
        //blastEffect.Play();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        //float playBackTime = blastEffect.main.duration;
        //yield return new WaitForSecondsRealtime(playBackTime);
        yield return null;
        Destroy(this.gameObject);
    }

    public void Attack(){
        //if(Player == Visible){
        //    StartCoroutine(ReleaseBullet());
        //}
    }

    private IEnumerator ReleaseBullet(){
        // BulletServiceInstance.GenerateBullet();
        yield return new WaitForSeconds(3f);
    }
}
