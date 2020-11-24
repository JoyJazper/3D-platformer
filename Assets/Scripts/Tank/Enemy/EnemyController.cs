using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour, IDamagable
{
    private float speed = 0.8f;
    public float Speed{
        set{
            speed = value;
        }
    }

    private float thrust = 0.5f;
    public float Thrust{
        set{
            thrust = value;
        }
    }

    private int health = 100;
    public int Health{
        set{
            health = value;
        }
    }

    public void Damage(int damage){
        health = health - damage;
        if(health <= 0){
            StartCoroutine(blastTank());
        }
    }

    private IEnumerator blastTank(){
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        yield return null;
        Destroy(this.gameObject);
    }
}
