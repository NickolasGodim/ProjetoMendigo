using UnityEngine;

public class Player : MonoBehaviour
{
    // Refer�ncia ao CharacterController da Unity
    private CharacterController controller;

    // Velocidade de movimento
    public float speed = 5f;

    // For�a do pulo
    public float jumpForce = 8f;

    // Gravidade aplicada ao personagem
    public float gravity = -9.81f;

    // Velocidade vertical (queda, pulo, etc.)
    private float verticalVelocity;

    // Refer�ncia ao Animator (para anima��es)
    private Animator anim;

    // Refer�ncia � c�mera (para rota��o com o mouse)
    public Transform cameraTransform;

    // Sensibilidade do mouse
    public float mouseSensitivity = 2f;

    // Acumulador para rota��o vertical (c�mera)
    private float xRotation = 0f;

    // Vari�vel para controlar o estado de pulo
    private bool isJumping = false;

    private float normalSpeed = 5f;
    private float runSpeed = 9f;

    // ----- IN�CIO DA PARTE NOVA: C�meras para cutscene e player -----
    [Header("C�meras Cutscene / Player")]
    public GameObject cameraCutscene;  // c�mera da Timeline / cutscene
    public GameObject cameraPlayer;    // c�mera principal do player
    // ----- FIM DA PARTE NOVA -----

    void Start()
    {
        // Pegando os componentes necess�rios na cena
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // Bloqueia e esconde o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;

        // ----- IN�CIO DA PARTE NOVA: Configura c�meras no come�o -----
        if (cameraCutscene != null && cameraPlayer != null)
        {
            cameraCutscene.SetActive(true);
            cameraPlayer.SetActive(false);
        }
        // ----- FIM DA PARTE NOVA -----

    }

    void Update()
    {
        Move();

        // ----------- PULO ---------------------
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // "cola" no ch�o

            // Se o personagem est� no ch�o e estava pulando, desativa a anima��o de pulo
            if (isJumping)
            {
                anim.ResetTrigger("pular"); // Reseta o trigger de pulo
                isJumping = false;  // Atualiza o estado para "n�o est� pulando"
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            isJumping = true;  // Atualiza o estado para "est� pulando"

            // Ativa anima��o de pulo (trigger)
            anim.SetTrigger("pular");
        }

        // Aplica gravidade
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalMove = Vector3.up * verticalVelocity;
        controller.Move(verticalMove * Time.deltaTime);

        // ----------- ROTACIONA O PLAYER COM O MOUSE (C�MERA) ----------------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Roda o player horizontalmente (gira no eixo Y)
        transform.Rotate(Vector3.up * mouseX);

        // Roda a c�mera verticalmente (limitada para n�o girar demais)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 20f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }

    public void Move()
    {
        // ----------- MOVIMENTA��O ----------------
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        // Ajusta a velocidade ao segurar Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;  // runSpeed j� declarado no seu c�digo (8f)
        }
        else
        {
            speed = normalSpeed;  // normalSpeed j� declarado no seu c�digo (5f)
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ----------- ANIMA��O DE WALK/IDLE -------------
        if (move != Vector3.zero && controller.isGrounded)
        {
            controller.Move(speed * Time.deltaTime * move); // Aplica movimenta��o
            anim.SetBool("caminhar", true);
        }
        else
        {
            anim.SetBool("caminhar", false);
        }
    }

    // ----- IN�CIO DA PARTE NOVA: M�todo chamado pelo Signal Receiver -----
    public void OnCutsceneEnd()
    {
        if (cameraCutscene != null && cameraPlayer != null)
        {
            cameraCutscene.SetActive(false);
            cameraPlayer.SetActive(true);
        }
    }
    // ----- FIM DA PARTE NOVA -----
}
