using UnityEngine;
public class TankView : MonoBehaviour
{
    #region Player movement Physics

        private Vector3 playerPosition;
        
        private void Start() {
            playerPosition = transform.position;
        }
        private void FixedUpdate(){
            MovePlayer();
        }

        private void MovePlayer(){
            playerPosition = transform.position;
            Vector3 movement = new Vector3(horizontalMove, 0, verticalMove);
            transform.position = playerPosition  + movement;
            transform.rotation = Quaternion.LookRotation(movement*10);
        }

    #endregion
    

    #region Player Movement Input System

        private float horizontalMove;
        private float verticalMove;
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
            horizontalMove = joystick.Horizontal*0.1f;
            verticalMove = joystick.Vertical*0.1f;
        }

    #endregion
}
