using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private float AnimBlendSpeed = 8.9f;
    private Rigidbody _playerRigidbody;
    private InputManager _inputManager;
    private Animator _animator;
    private bool _hasAnimator;
    private int _xVelHash;
    private int _yVelHash;
    private int _crouchHash;
    private int _reloadHash;
    private int _gunPlayHash;
    private int _jumpHash;
    private int _aimHash;
    private const float _walkSpeed = 5f;
    private const float _runSpeed = 10f;
    private Vector2 _currentVelocity;

    [Header("Cam Settings")]
    [SerializeField] Transform kafaHedefi, kafaBakis;
    [SerializeField] private Transform CameraRoot;
    [SerializeField] private Camera Cam;
    [SerializeField] private float UpperLimit = -40f;
    [SerializeField] private float BottomLimit = 70f;
    [SerializeField] private float MouseSensitivity = 21.9f;

    private float kafaAci = 90f;

    [Header("Rifle Settings")]
    [SerializeField] private Transform point;
    [SerializeField] private float range;
    [SerializeField] private float bulletDamage = 5f;
    [SerializeField] private float atesEtmeSikligi;
    [SerializeField] private float atesEtmeSikligi2;
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] public int toplamMermi;
    [SerializeField] private int jarjorKapistesi;
    [SerializeField] private int kalanMermi;
    [SerializeField] public TextMeshProUGUI toplamMermi_Text;
    [SerializeField] private TextMeshProUGUI kalanMermi_Text;
    [SerializeField] private Image headBar;

    private bool canShoot = true;

    [Header("health Settings")]
    private float health;

    private void Start()
    {
        health = 100f;

        kalanMermi = jarjorKapistesi;
        toplamMermi_Text.text = toplamMermi.ToString();

        _hasAnimator = TryGetComponent<Animator>(out _animator);
        _playerRigidbody = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();

        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
        _crouchHash = Animator.StringToHash("Crouch");
        _reloadHash = Animator.StringToHash("Reload");
        _gunPlayHash = Animator.StringToHash("GunPlay");
        _jumpHash = Animator.StringToHash("Jump");
        _aimHash = Animator.StringToHash("Aim");
    }

    private void Update()
    {
        bool isDeathAnim = _animator.GetCurrentAnimatorStateInfo(0).IsName("Death");

        if (!isDeathAnim)
        {
            if (Input.GetMouseButton(1))
            {
                Aim();
            }
           
            Jump();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }     
    }

    private void FixedUpdate()
    {
        bool isDeathAnim = _animator.GetCurrentAnimatorStateInfo(0).IsName("Death");

        if(!isDeathAnim)
        {
            Move();
            HandleCrouch();
            Reload();       
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void LateUpdate()
    {
        bool isDeathAnim = _animator.GetCurrentAnimatorStateInfo(0).IsName("Death");

        if (!isDeathAnim)
        {
            CamMovements();

            if (Input.GetMouseButton(0) && canShoot)
            {
                if (Time.time > atesEtmeSikligi && kalanMermi != 0)
                {
                    GunPlay();

                    atesEtmeSikligi = Time.time + atesEtmeSikligi2;
                }

                if (kalanMermi == 0)
                {
                    sources[3].Play();
                }
            }
        }
        
    }

    private void Move()
    {
        if (!_hasAnimator) return;

        float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
        if (_inputManager.Crouch) targetSpeed = 1.5f;
        if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

        _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
        _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

        var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
        var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

        _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)));

        _animator.SetFloat(_xVelHash, _currentVelocity.x);
        _animator.SetFloat(_yVelHash, _currentVelocity.y);
    }

    private void CamMovements()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * horizontal);

        kafaAci += vertical;
        kafaAci = Mathf.Clamp(kafaAci, 0, 180);

        Vector3 kafaPos = new Vector3(0, Mathf.Sin(kafaAci * Mathf.Deg2Rad), Mathf.Cos(kafaAci * Mathf.Deg2Rad));
        kafaHedefi.transform.localPosition = kafaPos;

        Vector3 kafaBakisPos = new Vector3(0, Mathf.Sin((kafaAci - 90) * Mathf.Deg2Rad), Mathf.Cos((kafaAci - 90) * Mathf.Deg2Rad));
        kafaBakis.transform.localPosition = kafaBakisPos;      
    }

    private void Jump()
    {
        _animator.SetBool(_jumpHash, _inputManager.Jump);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _animator.Play("Jump");
        }
    }

    private void HandleCrouch() => _animator.SetBool(_crouchHash, _inputManager.Crouch);

    private void GunPlay()
    {
        _animator.SetBool(_gunPlayHash, _inputManager.GunPlay);

        kalanMermi--;
        kalanMermi_Text.text = kalanMermi.ToString();

        _animator.Play("GunPlay");
        canShoot = false;
        sources[0].Play();
        sources[1].Play();
        Instantiate(particleSystems[0], point);

        StartCoroutine(ResetShootFlag());

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Instantiate(particleSystems[2], hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.collider.CompareTag("Düþman"))
            {
                hit.transform.gameObject.GetComponent<NPC>().SaglikDurumu(bulletDamage);
                Instantiate(particleSystems[1], hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    private void Aim()
    {
        _animator.SetBool(_aimHash, _inputManager.Aim);
        _animator.Play("Aim");
    }

    private void Reload()
    {
        _animator.SetBool(_reloadHash, _inputManager.Reload);
       
        if (Input.GetKey(KeyCode.R) && kalanMermi < jarjorKapistesi && toplamMermi != 0 && !sources[2].isPlaying && canShoot)
        {
            _animator.Play("Reload");
            canShoot = false;
            sources[2].Play();

            StartCoroutine(ResetCanShoot());
        }

        if (_animator.GetBool("ReloadUI"))
        {
            ReloadControl();
        }
    } 

    private void ReloadControl()
    {
        if (kalanMermi == 0)
        {
            if (toplamMermi <= jarjorKapistesi)
            {
                kalanMermi = toplamMermi;
                toplamMermi = 0;
            }
            else
            {
                toplamMermi -= jarjorKapistesi;
                kalanMermi = jarjorKapistesi;
            }
        }
        else
        {
            if (toplamMermi <= jarjorKapistesi)
            {
                int olusanDeger = kalanMermi + toplamMermi;

                if (olusanDeger > jarjorKapistesi)
                {
                    kalanMermi = jarjorKapistesi;
                    toplamMermi = olusanDeger - jarjorKapistesi;
                }
                else
                {
                    kalanMermi += toplamMermi;
                    toplamMermi = 0;
                }
            }
            else
            {
                int mevcutMermi = jarjorKapistesi - kalanMermi;
                toplamMermi -= mevcutMermi;
                kalanMermi += mevcutMermi;
            }
        }

        toplamMermi_Text.text = toplamMermi.ToString();
        kalanMermi_Text.text = kalanMermi.ToString();

        _animator.SetBool("ReloadUI", false);
    }

    public void SaglikDurumu(float darbeGücü)
    {
        health -= darbeGücü;
        headBar.fillAmount = health / 100;

        if (health <= 0)
        {
            _animator.SetBool("Death", true);
        }
    }

    private IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(.12f);
        canShoot = true;
    }

    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(3.7f);
        canShoot = true;
    }
}
