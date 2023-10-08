using UnityEngine;

public class TürkAl : MonoBehaviour
{
    [Header("Animation Settings")]
    private Animator _animator;

    [Header("Rifle Settings")]
    [SerializeField] private float atesEtmeMenzil;
    [SerializeField] private GameObject point;
    [SerializeField] private float atesEtmeSikligi;
    [SerializeField] private float atesEtmeSikligi2;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private AudioSource source;
    [SerializeField] private float darbeGücü = 100;
    private bool atesEdiliyormu = false;

    [Header("health Settings")]
    private float health;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        health = 100f;
    }

    private void LateUpdate()
    {
        AtesEtmeMenzili();
    }

    private void AtesEtmeMenzili()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, atesEtmeMenzil);

        foreach (var dusmanlar in hitColliders)
        {
            if (dusmanlar.gameObject.CompareTag("Yunan"))
            {
                bool isDeathAnim = _animator.GetCurrentAnimatorStateInfo(0).IsName("Death");

                if (!isDeathAnim)
                {
                    AtesEt(dusmanlar.gameObject);
                }
            }
            else
            {
                if (atesEdiliyormu)
                {
                    _animator.SetBool("Rifle", false);
                    atesEdiliyormu = false;
                }
            }
        }
    }

    void AtesEt(GameObject hedef)
    {
        atesEdiliyormu = true;
        _animator.SetBool("Rifle", true);

        Vector3 aradakiFark = hedef.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(aradakiFark, Vector3.up);
        transform.rotation = rotation;

        RaycastHit hit;
        if (Physics.Raycast(point.transform.position, point.transform.forward, out hit, atesEtmeMenzil))
        {
            Color color = Color.red;
            Vector3 degisenPosizyon = new Vector3(hedef.transform.position.x, hedef.transform.position.y + 1.4f, hedef.transform.position.z);
            Debug.DrawLine(point.transform.position, degisenPosizyon, color);

            if (Time.time > atesEtmeSikligi)
            {
                particleSystems[1].Play();

                if (!source.isPlaying)
                {
                    source.Play();
                }

                YunanAl yunanAlComponent = hit.transform.gameObject.GetComponent<YunanAl>();

                if (yunanAlComponent != null)
                {
                    yunanAlComponent.SaglikDurumu(darbeGücü);
                }

                Instantiate(particleSystems[0], hit.point, Quaternion.LookRotation(hit.normal));
                atesEtmeSikligi = Time.time + atesEtmeSikligi2;
            }
        }
    }

    public void SaglikDurumu(float darbeGücü)
    {
        health -= darbeGücü;

        if (health <= 0)
        {
            _animator.SetBool("Death", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, atesEtmeMenzil);
    }
}
