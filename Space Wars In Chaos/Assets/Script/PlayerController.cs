using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _Speed;
    [SerializeField] private float _XminPos;
    [SerializeField] private float _XmaxPos;
    [SerializeField] private float _YminPos;
    [SerializeField] private float _YmaxPos;

    [SerializeField] private float _PitchFactor;
    [SerializeField] private float _PitchControlFactor;

    [SerializeField] private float _YawFactor;
    [SerializeField] private float _RollControlFactor;
    // Start is called before the first frame update

    private Vector3 origLocalPos;
    void Start()
    {
        origLocalPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        //move
        Move(horizontalInput, verticalInput);
        AutoRotate(horizontalInput, verticalInput);
    }

    // move the player ship
    void Move(float xInput, float yInput){
        Vector3 offSetXpos = (Vector3.right*xInput)*_Speed *Time.deltaTime;
        Vector3 offSetYpos = (Vector3.up*yInput)*_Speed *Time.deltaTime;
        Vector3 newPos = this.transform.localPosition + offSetXpos + offSetYpos;

        //clamped
        newPos.x = Mathf.Clamp(newPos.x, _XminPos, _XmaxPos);
        newPos.y = Mathf.Clamp(newPos.y, _YminPos, _YmaxPos);

        //set
        this.transform.localPosition = newPos;
    }

    void AutoRotate(float xInput, float yInput){
        //use pitch, yaw, and roll system
        //start calculation based on center position
        Vector3 originOffSet = this.transform.localPosition - origLocalPos;
        //calculations
        float pitchOnPos = (originOffSet.y * -_PitchFactor);
        float pitchOnThrowInput = (yInput * -_PitchControlFactor);

        float pitch = pitchOnPos + pitchOnThrowInput;
        float yaw = (originOffSet.x * _YawFactor);
        float roll = (xInput * _RollControlFactor);

        //set to local rotation
        this.transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
