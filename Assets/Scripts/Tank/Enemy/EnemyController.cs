using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour, Idamagable, IAttacker
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

    private TankService tankService;
    private Transform player;

    private void Start(){
        tankService = TankService.Instance;
        player = tankService.PlayerTransform;
    }

    private void LateUpdate(){
        transform.LookAt(player);
    }

    public void Damage(int damage){
        health = health - damage;
        if(health <= 0){
            // TODO : apply animations named death in the animator of enemy using particle system.

        }
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
