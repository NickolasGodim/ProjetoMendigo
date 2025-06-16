using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 5f;

    public float jumpForce = 8f;

    public float gravity = -9.81f;

    private float verticalVelocity;

    private Animator anim;

    public Transform cameraTransform;

    public float mouseSensitivity = 2f;

    private float xRotation = 0f;

    private bool isJumping = false;

    private float normalSpeed = 5f;
    private float runSpeed = 9f;

    [Header("Câmeras Cutscene / Player")]
    public GameObject cameraCutscene;  
    public GameObject cameraPlayer;   
   

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;

        if (cameraCutscene != null && cameraPlayer != null)
        {
            cameraCutscene.SetActive(true);
            cameraPlayer.SetActive(false);
        }

    }

    void Update()
    {
        Move();

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;

            if (isJumping)
            {
                anim.ResetTrigger("pular"); 
                isJumping = false;  
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            isJumping = true;  

           
            anim.SetTrigger("pular");
        }

        
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMove = Vector3.up * verticalVelocity;
        controller.Move(verticalMove * Time.deltaTime);

        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        
        transform.Rotate(Vector3.up * mouseX);

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 20f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }

    public void Move()
    {
        
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;  
        }
        else
        {
            speed = normalSpeed;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move != Vector3.zero && controller.isGrounded)
        {
            controller.Move(speed * Time.deltaTime * move); 
            anim.SetBool("caminhar", true);
        }
        else
        {
            anim.SetBool("caminhar", false);
        }
    }

    public void OnCutsceneEnd()
    {
        if (cameraCutscene != null && cameraPlayer != null)
        {
            cameraCutscene.SetActive(false);
            cameraPlayer.SetActive(true);
        }
    }
}
