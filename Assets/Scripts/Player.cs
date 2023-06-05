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
        Vector3 movDir= new Vector3(inputVector.x,0f,inputVector.y);
        // transform.position += (Vector3)inputVector; 
        transform.position += movDir*moveSpeed* Time.deltaTime; 
        isWalking = movDir!=Vector3.zero;
        // Debug.Log(Time.deltaTime);
        transform.forward =Vector3.Slerp(transform.forward,movDir,rotateSpeed*Time.deltaTime) ;
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}
