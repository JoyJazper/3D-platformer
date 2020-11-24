using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class TankController : MonoBehaviour, IDamagable
{
    private float horizontalMove;
    private float verticalMove;
    private float fireForceValue;
    private Rigidbody playerBody;
    private float speed = 0.8f;
    private float thrust = 0.8f;
    private int health = 100;

    private float rotationInputBuffer = 0.2f;
    private int rotationMultiplierValue = 3;

    [SerializeField]
    private Transform bulletHandle;
    [SerializeField]
    private ParticleSystem blastTankEffect;
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
    
    private Slider fireForce;
    public Slider FireForce{
        set{
            fireForce = value;
        }
    }
    
    private void Start(){
        PrepareInput();
    }

    private void Update(){
        InputToMove();
    }

    private void FixedUpdate() {
        MovePlayer();
        RotatePlayer();
    }

    #region Movement Physics
        private void PrepareInput(){
            playerBody =gameObject.GetComponent<Rigidbody>();
            fireButton.onClick.AddListener(Fire);
            fireForce.onValueChanged.AddListener(delegate {SetFireForceValue();});
        }

        private void MovePlayer(){
            playerBody.AddForce(transform.forward*verticalMove, ForceMode.VelocityChange);
        }

        private void RotatePlayer(){
            Vector3 rotationValue = new Vector3(0, horizontalMove, 0);
            if(horizontalMove > rotationInputBuffer || horizontalMove < -rotationInputBuffer){
                transform.Rotate(0f, horizontalMove*rotationMultiplierValue, 0f);
            }
        }

    #endregion

    #region Movement Input

        private void InputToMove(){
            horizontalMove = joystick.Horizontal*thrust;
            verticalMove = joystick.Vertical*speed;
        }

    #endregion

    #region Bullet Fire
        private void SetFireForceValue(){
            fireForceValue = fireForce.value; 
        }

        private void Fire(){
            BulletService instance = BulletService.Instance;
            instance.FireBullet(weaponsList[0], bulletHandle, fireForceValue); 
        }

    #endregion
    
    #region Tank Health Management
        public void Damage(int damage){
            health = health - damage;
            if(health <= 0){
                KillTank();
            }
        }

        private void KillTank(){
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(blastTank(blastTankEffect));
        }

        private IEnumerator blastTank(ParticleSystem blastEffect){
            blastEffect.Play();
            float playBackTime = blastEffect.main.duration;
            yield return new WaitForSecondsRealtime(playBackTime);
            Destroy(this.gameObject);
        }

    #endregion
}
