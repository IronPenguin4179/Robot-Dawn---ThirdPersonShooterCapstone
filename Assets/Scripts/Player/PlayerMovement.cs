using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    //Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 500f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent <Animator> ();
        playerRigidbody = GetComponent <Rigidbody> ();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); //raw means it is either moving or not, not transitional move amounts. always full speed.
        float v = Input.GetAxisRaw("Vertical"); //what is considered input is defaulted to the input manager in unity.
        Turning();
        Move(h, v);
        //Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime; //Time is required to make movement update at realisitic interval. normalized is so diagonal isn't faster.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // Makes playerToMouse orientation the basis for the characters rotation. Character always turns with mouse position.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    /*void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }*/


}
