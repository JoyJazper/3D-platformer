using UnityEngine;
public class TankView : MonoBehaviour
{
    #region Player movement Physics

        private Vector3 playerPosition;
        private Rigidbody playerBody;
        
        private void Start() {
            playerPosition = transform.position;
            playerBody = gameObject.GetComponent<Rigidbody>();
        }
        private void FixedUpdate(){
            MovePlayer();
        }

        private void MovePlayer(){
            playerBody.AddForce(transform.forward*verticalMove, ForceMode.VelocityChange);
            Vector3 rotationValue = new Vector3(0, horizontalMove, 0);
            if(horizontalMove > 0.2 || horizontalMove < -0.2 ){
                transform.Rotate(0f, horizontalMove*3, 0f);
            }
        }

    #endregion
    

    #region Player Movement Input System

        private float horizontalMove;
        private float verticalMove;
        
        private float speed;
        public float Speed{
            set{
                speed = value;
            }
        }

        private float thrust;
        public float Thrust{
            set{
                thrust = value;
            }
        }

        private Joystick joystick;
        public Joystick Joystick{
            set{
                joystick = value;
            }
        }

        private void Update() {
            InputToMove();
        }

        private void InputToMove(){
            horizontalMove = joystick.Horizontal*thrust;
            verticalMove = joystick.Vertical*speed;
        }

    #endregion
}
