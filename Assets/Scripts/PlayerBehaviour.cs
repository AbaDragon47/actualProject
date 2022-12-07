using Unity.Netcode;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    
    public playerHealth healthBar;
    public int maxHealth = 100;
    public int currrentHealth;

    public Camera x;
    float playerHeight = 2f;

    [Header("Movement")]
 
    private float speed = 1f;
    [SerializeField] float movementMult=10f;
    [SerializeField] float airMult = 0.4f;
    //rbDrag = 6f;
    

    [Header("jumping")]
    public float jumpForce=5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    //private NetworkVariable<float> fBpos = new NetworkVariable<float>();
    //private NetworkVariable<float> lRpos = new NetworkVariable<float>();


    //private float turnSpeed = 45;
    private float hInput;
    private float fInput;
    //movementMult = 5f;
    // Update is called once per frame
    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirect;
    Rigidbody rb;


    RaycastHit slopeHit;
    
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out slopeHit, playerHeight/2 + 1f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        currrentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
        if(IsOwner&&IsLocalPlayer)
            StartCam();
    }
    void ControlDrag()
    {
        // drag prevents the sliding on the ground even when not moving
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }
    private void Update()
    {

        UpdateClient();
        UpdateServer();
        if (IsLocalPlayer)
        {
            CmdPosServerRpc(transform.position);
            x.enabled = true;
        }
        else
            x.enabled = false;
            
    }

    [ServerRpc]
    void CmdPosServerRpc(Vector3 position)
    {
        // we trust the player :)
        transform.position = position;
    }

   /* public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        writer.Write(transform.position);
        return true;
    }

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        if (IsLocalPlayer)
        {
            return;
        }

        transform.position = reader.ReadVector3();
    }*/

    void UpdateClient()
    {
        if (!IsOwner)
            return;
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        //debug.log(isGrounded);

        Movement();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)    
        {
            Jump();
        }
        else
        {
            rb.AddForce(Vector3.down*10f,ForceMode.Acceleration);
            //Debug.Log("going down");
        }
        slopeMoveDirect = Vector3.ProjectOnPlane(moveDirection,slopeHit.normal);
        
        
        //move forward
       
       // Debug.Log(fInput);
      //  transform.Translate(Vector3.up * Time.deltaTime * speed * fInput);
        //transform.Translate(Vector3.right * Time.deltaTime * speed * hInput );
    }
    void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x+hInput, transform.position.y, transform.position.z+fInput);
    }

    private void Jump()
    {
        Debug.Log("can jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
    }

    void Movement()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        fInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * fInput + transform.right * hInput;
    }
    
    private void FixedUpdate()
    {
        if(IsOwner&&IsLocalPlayer)
            MovePlayerServerRpc();
    }

    [ServerRpc]
    private void MovePlayerServerRpc()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized*speed*movementMult, ForceMode.VelocityChange);
        }
        else if(isGrounded&& OnSlope())
        {
            rb.AddForce(slopeMoveDirect.normalized * speed * movementMult, ForceMode.VelocityChange);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMult*airMult, ForceMode.VelocityChange);
        }
        
    }
    // Follow Player
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;


    [SerializeField] Camera cam;

    float mouseX;
    float mouseY;

    float multi = .01f;
    float xRot, yRot;

    private void StartCam()
    {

        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UpdateCam()
    {
        // myinput gets mouse position and ig basis visiion of player based on mouse
        if(IsOwner&& IsLocalPlayer)
            MyInputServerRpc();
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    [ServerRpc]
    void MyInputServerRpc()
    {


        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multi;
        xRot -= mouseY * sensY * multi;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
}
