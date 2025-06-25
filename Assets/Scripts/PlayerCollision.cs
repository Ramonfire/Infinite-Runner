using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] string CoinTag = "Coin"; //match it to the editor
    [SerializeField] string HealthTag = "Health";//match it to the editor
    [SerializeField] string DamageTag = "Damage";//match it to the editor
    [SerializeField] string TriggerName = "Hit";//match it to the editor
    [SerializeField] Animator ModelAnimator;
    [SerializeField] float CoolDownHit=1.0f;
    float lastHit=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ModelAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool CheckHitCoolDown()
    {
        return (Time.time - lastHit > 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(DamageTag) && CheckHitCoolDown())
        {
            ModelAnimator.SetTrigger("Hit");
            lastHit = Time.time;
        }

    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CoinTag))
        {
            Debug.Log("Coin Picked Up");
            other.gameObject.SetActive(false);// dont destroy the object since it is childed to the Path Chunk
        }

        if (other.gameObject.CompareTag(HealthTag))
        {
            Debug.Log("Healing");
            other.gameObject.SetActive(false);// dont destroy the object since it is childed to the Path Chunk
        }

        if (other.gameObject.CompareTag(DamageTag) && CheckHitCoolDown())
        {
            ModelAnimator.SetTrigger("Hit");
            lastHit = Time.time;
        }
    }
}
