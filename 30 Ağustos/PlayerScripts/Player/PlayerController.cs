using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private float AnimBlendSpeed = 8.9f;
    [SerializeField] private Cinemachine.CinemachineFreeLook freeLookCamera;
    private Rigidbody _playerRigidbody;
    private InputManager _inputManager;
    private Animator _animator;
    private bool _hasAnimator;
    private int _xVelHash;
    private int _yVelHash;
    private int _crouchHash;
    private int _reloadHash;
    private int _firingHash;
    private const float _walkSpeed = 2f;
    private const float _runSpeed = 6f;
    private Vector2 _currentVelocity;

    [Header("Rifle Settings")]
    [SerializeField] private Transform point;
    [SerializeField] private float range;
    [SerializeField] private float bulletDamage = 100f;
    [SerializeField] private float atesEtmeSikligi;
    [SerializeField] private float atesEtmeSikligi2;
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private GameObject cephaneBilgi;
    [SerializeField] private int toplamMermi;
    [SerializeField] private int jarjorKapistesi;
    [SerializeField] private int kalanMermi;
    [SerializeField] private TextMeshProUGUI toplamMermi_Text;
    [SerializeField] private TextMeshProUGUI kalanMermi_Text;
    [SerializeField] private Image headBar;

    private int cephaneSayisi = 20;
    private bool cephaneAlma = false;
    private bool canShoot = true;
    private Camera cam;

    [Header("health Settings")]
    private float health;

    [Header("GameOver")]
    [SerializeField] private GameObject[] paneller;
    
    private void Start()
    {
        cam = Camera.main;
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
        _firingHash = Animator.StringToHash("Firing");
    }

    private void FixedUpdate()
    {
        bool isDeathAnim = _animator.GetCurrentAnimatorStateInfo(0).IsName("Death");

        if(!isDeathAnim)
        {
            Move();
            HandleCrouch();
            Reload();
            Firing();
            Cephane();
        }
        else
        {
            paneller[0].SetActive(true);
            paneller[1].SetActive(false);
            paneller[2].SetActive(false);

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

            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                if (Time.time > atesEtmeSikligi && kalanMermi != 0)
                {
                    AtesEt();

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
        float cameraYaw = freeLookCamera.m_XAxis.Value;
        _playerRigidbody.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
    }

    private void HandleCrouch() => _animator.SetBool(_crouchHash, _inputManager.Crouch);

    private void Firing() => _animator.SetBool(_firingHash, _inputManager.Firing);

    private void AtesEt()
    {
        kalanMermi--;
        kalanMermi_Text.text = kalanMermi.ToString();

        canShoot = false;
        sources[0].Play();
        sources[1].Play();
        Instantiate(particleSystems[0], point);

        StartCoroutine(ResetShootFlag());

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Yunan"))
            {
                hit.transform.gameObject.GetComponent<YunanAl>().SaglikDurumu(bulletDamage);
                Instantiate(particleSystems[1], hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.gameObject.GetComponent<CapsuleCollider>().direction = 0;
                hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                Instantiate(particleSystems[2], hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    private void Reload()
    {
        _animator.SetBool(_reloadHash, _inputManager.Reload);

        if (Input.GetKey(KeyCode.R) && kalanMermi < jarjorKapistesi && toplamMermi != 0 && !sources[2].isPlaying && canShoot)
        {
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

    private void Cephane()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5.5f))
        {
            if(hit.transform.CompareTag("Cephane"))
            {
                cephaneAlma = true;
                cephaneBilgi.SetActive(true);

                if(Input.GetKey(KeyCode.E) && cephaneAlma == true && !sources[4].isPlaying)
                {
                    if(toplamMermi < 20)
                    {
                        toplamMermi += (cephaneSayisi - toplamMermi);
                        sources[4].Play();
                    }
                    else
                    {
                        cephaneAlma = false;
                    }

                    toplamMermi_Text.text = toplamMermi.ToString();
                }
            }
        }
        else
        {
            cephaneBilgi.SetActive(false);
        }
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
        yield return new WaitForSeconds(2.612f);
        canShoot = true;
    }

    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(2.9f);
        canShoot = true;
    }
}
