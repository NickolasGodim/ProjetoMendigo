using UnityEngine;

public class Player : MonoBehaviour
{
    // Referência ao CharacterController da Unity
    private CharacterController controller;

    // Velocidade de movimento
    public float speed = 5f;

    // Força do pulo
    public float jumpForce = 8f;

    // Gravidade aplicada ao personagem
    public float gravity = -9.81f;

    // Velocidade vertical (queda, pulo, etc.)
    private float verticalVelocity;

    // Referência ao Animator (para animações)
    private Animator anim;

    // Referência à câmera (para rotação com o mouse)
    public Transform cameraTransform;

    // Sensibilidade do mouse
    public float mouseSensitivity = 2f;

    // Acumulador para rotação vertical (câmera)
    private float xRotation = 0f;

    // Variável para controlar o estado de pulo
    private bool isJumping = false;

    void Start()
    {
        // Pegando os componentes necessários na cena
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // Bloqueia e esconde o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();

        // ----------- PULO ---------------------
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // "cola" no chão

            // Se o personagem está no chão e estava pulando, desativa a animação de pulo
            if (isJumping)
            {
                anim.ResetTrigger("pular"); // Reseta o trigger de pulo
                isJumping = false;  // Atualiza o estado para "não está pulando"
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            isJumping = true;  // Atualiza o estado para "está pulando"

            // Ativa animação de pulo (trigger)
            anim.SetTrigger("pular");
        }

        // Aplica gravidade
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMove = Vector3.up * verticalVelocity;
        controller.Move(verticalMove * Time.deltaTime);

        // ----------- ROTACIONA O PLAYER COM O MOUSE (CÂMERA) ----------------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Roda o player horizontalmente (gira no eixo Y)
        transform.Rotate(Vector3.up * mouseX);

        // Roda a câmera verticalmente (limitada para não girar demais)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 20f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void Move()
    {
        // ----------- MOVIMENTAÇÃO ----------------
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ----------- ANIMAÇÃO DE WALK/IDLE -------------
        // Se estiver se movendo, ativa a animação de corrida (trigger bool)
        if (move != Vector3.zero && controller.isGrounded)
        {
            controller.Move(speed * Time.deltaTime * move); // Aplica movimentação
            anim.SetBool("caminhar", true);
        }
        else
        {
            anim.SetBool("caminhar", false);
        }
    }
}
