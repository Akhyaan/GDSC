using UnityEngine;

public class CameraController : MonoBehaviour
{
    //room camera
    [SerializeField] private float speed=0.1f;
    private float currentPosX;
    private Vector3 velocity= Vector3.zero;

    //follow player
    [SerializeField] private Transform Player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camSpeed;
    private float lookAhead;



    private void Update()
    {   
        //room camera
        //transform.position= Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y,transform.position.z),ref velocity,speed);

        //follow player
        transform.position=new Vector3(Player.position.x+lookAhead, transform.position.y, transform.position.z );
        lookAhead=Mathf.Lerp(lookAhead,(aheadDistance*Player.localScale.x),Time.deltaTime*camSpeed);

    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX=_newRoom.position.x;
    }


}
