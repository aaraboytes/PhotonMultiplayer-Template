using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _turnSmoothTime = 0.1f;
    [SerializeField] float _distanceToFloor = 0.1f;
    [SerializeField] Animator _animator;
    [SerializeField] Renderer _renderer;
    [SerializeField] GameObject _cineMachine;
    public bool ControlledByAxis { get; set; }
    private bool isInAir = false;
    private float turnSmoothVelocity;
    private Rigidbody body;
    private Transform cam;
    private Vector2 axisInput;
    private Vector3 moveDir;
    private void Awake()
    {
        ControlledByAxis = true;
        body = GetComponent<Rigidbody>();
        _cineMachine.SetActive(base.photonView.IsMine);
    }
    private void Start()
    {
        cam = Camera.main.transform;
    }
    void Update()
    {
        if (base.photonView.IsMine)
        {
            if(ControlledByAxis)
                axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 direction = new Vector3(axisInput.x, 0, axisInput.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                moveDir = (Quaternion.Euler(0, targetAngle, 0) * (axisInput.y>=0?Vector3.forward:Vector3.back)).normalized;
            }
            else
            {
                moveDir = new Vector3();
            }
            
            if (Input.GetButtonDown("Jump") && !isInAir)
            {
                Jump();
            }

            if(isInAir)
                CheckGrounded();

            if (Input.GetKeyDown(KeyCode.E))
            {
                SendColorChange();
            }
        }
    }
    private void FixedUpdate()
    {
        body.velocity = new Vector3(moveDir.x * _speed,  body.velocity.y ,moveDir.z * _speed);
    }
    private void ChangeColor(float hue)
    {
        _renderer.material.color =Color.HSVToRGB(hue,1,1);
    }
    private void CheckGrounded()
    {
        Debug.Log("Checking grounded");
        Collider[] collisions = Physics.OverlapSphere(transform.position, _distanceToFloor);
        if(body.velocity.y < 0 && collisions.Length>0)
        {
            isInAir = false;
            _animator.SetTrigger("Land");
            Debug.Log("Landed");
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Receive position and rotation directly
        /*if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }*/
    }
    public void GiveHandler(LocalPlayerHandler handler)
    {
        if (photonView.IsMine)
            handler.SetPlayer(this);
    }
    public void Jump()
    {
        isInAir = true;
        _animator.SetTrigger("Jump");
        body.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    public void SendColorChange()
    {
        float hue = Random.value;
        photonView.RPC("RPC_ChangeColor",RpcTarget.All,PhotonNetwork.LocalPlayer, hue);
    }
    public void SetAxis(Vector2 axis)
    {
        axisInput = axis;
    }
    #region RPC
    [PunRPC]
    private void RPC_ChangeColor(Player player,float hue)
    {
        ChangeColor(hue);
    }
    #endregion
}
