using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class TankController : MonoBehaviour, Idamagable
{
    private float horizontalMove;
    private float verticalMove;

    private Rigidbody playerBody;

    [SerializeField]
    private float speed = 0.8f;

    [SerializeField]
    private float thrust = 0.8f;

    [SerializeField]
    private int health = 100;
    private Joystick joystick;
    public Joystick Joystick{
        set{
            joystick = value;
        }
    }

    #region Movement Physics
        private void Start(){
            playerBody =gameObject.GetComponent<Rigidbody>();
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
        }

        private void InputToMove(){
            horizontalMove = joystick.Horizontal*thrust;
            verticalMove = joystick.Vertical*speed;
        }

    #endregion

    public void Damage(int damage){
        health = health - damage;
        if(health > 0){
            // TODO: add animation to the player and enemy animators using particle system.
            //GetComponent<Animator>().Play("Death");
        }
    }
}
