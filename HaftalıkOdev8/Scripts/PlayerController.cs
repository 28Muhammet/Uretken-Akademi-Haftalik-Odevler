using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask mask;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        _controller.Move(move * speed * Time.deltaTime);
        #endregion

        #region Jump
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        #endregion

        #region Gravity
        _isGrounded = Physics.CheckSphere(ground.position, distance, mask);

        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        #endregion
    }
}
