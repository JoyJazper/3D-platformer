using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class TankController : MonoBehaviour, Idamagable
{
    private float horizontalMove;
    private float verticalMove;

    private Rigidbody playerBody;
    [SerializeField]
    private Transform bulletHandle;

    [SerializeField]
    private ParticleSystem blastTankEffect;

    [SerializeField]
    private float speed = 0.8f;

    [SerializeField]
    private float thrust = 0.8f;

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private List<BulletScriptableObject> weaponsList;
    private Joystick joystick;
    public Joystick Joystick{
        set{
            joystick = value;
        }
    }

    private Button fireButton;
    public Button FireButton{
        set{
            fireButton = value;
        }
    }
    private float fireForceValue;
    private Slider fireForce;
    public Slider FireForce{
        set{
            fireForce = value;
        }
    }
    
    

    #region Movement Physics
        private void Start(){
            playerBody =gameObject.GetComponent<Rigidbody>();
            fireButton.onClick.AddListener(Fire);
            fireForce.onValueChanged.AddListener(delegate {SetFireForceValue();});
        }

        private void FixedUpdate() {
            MovePlayer();
        }

        private void MovePlayer(){
            playerBody.AddForce(transform.forward*verticalMove, ForceMode.VelocityChange);
            Vector3 rotationValue = new Vector3(0, horizontalMove, 0);
            if(horizontalMove > 0.2 || horizontalMove < -0.2){
                transform.Rotate(0f, horizontalMove*3, 0f);
            }
        }

    #endregion

    #region Movement Input

        private void Update(){
            InputToMove();
            if(Input.GetKeyDown(KeyCode.Space)){
                BulletService instance = BulletService.Instance;
                instance.FireBullet(weaponsList[0], bulletHandle, fireForceValue); 
            }
        }

        private void InputToMove(){
            horizontalMove = joystick.Horizontal*thrust;
            verticalMove = joystick.Vertical*speed;
        }

    #endregion

    private void SetFireForceValue(){
        fireForceValue = fireForce.value; 
    }

    private void Fire(){
        BulletService instance = BulletService.Instance;
        instance.FireBullet(weaponsList[0], bulletHandle, fireForceValue); 
    }
    public void Damage(int damage){
        health = health - damage;
        if(health > 0){
            StartCoroutine(blastTank(blastTankEffect));
        }
    }

    private IEnumerator blastTank(ParticleSystem blastEffect){
        blastEffect.Play();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        float playBackTime = blastEffect.main.duration;
        yield return new WaitForSecondsRealtime(playBackTime);
        Destroy(this.gameObject);
    }
}
