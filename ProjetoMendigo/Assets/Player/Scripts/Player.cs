using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float RotSpeed;
    private float Rotation;
    public float Gravity;

    Vector3 MoveDirection; //Vector3 = aos pontos de movimentação, x y z
    CharacterController controller;
    //Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        //cada componente que adicionar no jogo, tem q estar aqui para referenciar ele
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (controller.isGrounded)//controler = CharacterController, o player
        {

            if (Input.GetKey(KeyCode.W))
            {
                MoveDirection = Vector3.forward * Speed; //MoveDirection = Vector3, logo x y z

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                MoveDirection = Vector3.zero;
                //transform.Translate(Vector3.back * Velocity * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveDirection = Vector3.back * Speed; //MoveDirection = Vector3, logo x y z

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                MoveDirection = Vector3.zero;
                //transform.Translate(Vector3.back * Velocity * Time.deltaTime);
            }
        }
        Rotation += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime; //rot é rotação, Time.deltaTime é para nao perder a velocidade
        transform.eulerAngles = new Vector3(0, Rotation, 0);

        MoveDirection.y -= Gravity * Time.deltaTime;//atualiza a gravidade conforme ao terreno
        MoveDirection = transform.TransformDirection(MoveDirection);
        controller.Move(MoveDirection * Time.deltaTime);
    }

}