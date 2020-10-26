using UnityEngine;
public class TankView : MonoBehaviour
{
    private void Start() {
        playerPosition = transform.position;
    }

    private void fixedUpdate(){
        MovePlayer();
    }

    private float horizontal;
    private float vertical;
    private Vector3 playerPosition;
    public void MovePlayer(){
        playerPosition = new Vector3(playerPosition.x + horizontal, playerPosition.y, playerPosition.z + vertical);
    }

    public void updateMovement(float updatedHorizontal, float updatedVertical){
        horizontal = updatedHorizontal;
        vertical = updatedVertical;
    }
}
