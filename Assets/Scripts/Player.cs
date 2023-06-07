using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isWalking;
    // public float moveSpeed;
    [SerializeField]private float moveSpeed =10f;
    [SerializeField]private float rotateSpeed;
    [SerializeField]private GameInput gameInput;

    // Update is called once per frame
    private void Update()
    {
        CharMove();
    }
    void CharMove()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir= new Vector3(inputVector.x,0f,inputVector.y);
        float moveDistance =moveSpeed* Time.deltaTime; ;
        float playerRadius=0.7f;
        float playerHeight=2f;
        // bool canMove = ! Physics.Raycast(transform.position,movDir,playerSize);
        bool canMove = ! Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDir,moveDistance);
        if(!canMove)
        {
            //cannot move towards moveDir

            //Attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;  
            canMove = ! Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDirX,moveDistance);
            if(canMove)
            {
                // can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the x
                // Attempt only z movement
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized; 
                canMove = ! Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDirZ,moveDistance);
                if(canMove)
                {
                    // Move only om the z axis
                    moveDir=moveDirZ;
                }
                 else
                {
                    // cannot move in any direction
                }
            }
        }
        if(canMove)
        {
        // transform.position += (Vector3)inputVector; 
        transform.position += moveDir*moveDistance;      
        }
        isWalking = moveDir!=Vector3.zero;
        // Debug.Log(Time.deltaTime);
        transform.forward =Vector3.Slerp(transform.forward,moveDir,rotateSpeed*Time.deltaTime) ;
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}
