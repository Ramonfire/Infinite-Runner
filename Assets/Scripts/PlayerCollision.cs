using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
[RequireComponent(typeof(PlayerController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] string CoinTag = "Coin"; //match it to the editor
    [SerializeField] string HealthTag = "Health";//match it to the editor
    [SerializeField] string DamageTag = "Damage";//match it to the editor
    [SerializeField] string CheckPointTag = "CheckPoint";//match it to the editor
    [SerializeField] string AnimationTriggerName = "Hit";//match it to the editor for animation
    [SerializeField] Animator ModelAnimator;
    [SerializeField] float CoolDownHit=1.0f;
    [SerializeField] float AdjsutSpeed=-2.0f;
    [SerializeField] int TimePenalty=8;
    float lastHit=0;
    LevelGenerator levelGenerator;
    PlayerController controller;
    ScoreManager scoreManager;
    TimeTracker timeTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ModelAnimator = GetComponentInChildren<Animator>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        controller = GetComponent<PlayerController>();

        //these two are on the same object
        scoreManager = FindFirstObjectByType<ScoreManager>();
        timeTracker = scoreManager.GetComponent<TimeTracker>();
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
        if (timeTracker.IsGameOver)
            return;

        if (collision.gameObject.CompareTag(DamageTag) && CheckHitCoolDown())
        {
            GotHit();
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if (timeTracker.IsGameOver)
            return;

        if (other.gameObject.CompareTag(CoinTag))
        {
            PickUpCoin(other);
            other.gameObject.SetActive(false);// dont destroy the object since it is childed to the Path Chunk
        }

        if (other.gameObject.CompareTag(HealthTag))
        {
            PowerUp(other);
            other.gameObject.SetActive(false);// dont destroy the object since it is childed to the Path Chunk
        }

        if (other.gameObject.CompareTag(DamageTag) && CheckHitCoolDown())
        {
            GotHit();
        }


        if (other.gameObject.CompareTag(CheckPointTag) && CheckHitCoolDown())
        {
            AddTime();
        }
    }

    private void AddTime()
    {
        timeTracker.AddTime(Random.Range(5, TimePenalty));// add from 5 to 7 seconds
    }

    private void GotHit()
    {
        ModelAnimator.SetTrigger("Hit");
        levelGenerator.ChangeLevelSpeedBy(AdjsutSpeed);
        controller.AdjustMoveSpeed(AdjsutSpeed);
        timeTracker.AddTime(-TimePenalty);
        lastHit = Time.time; 
    }


  
    private void PowerUp(Collider other)
    {
        Assert.IsTrue(other.gameObject.GetComponent<PickUp>().GetPickUp()==Type.health);
        levelGenerator.ChangeLevelSpeedBy(-AdjsutSpeed);
        controller.AdjustMoveSpeed(-AdjsutSpeed);
        timeTracker.AddTime(4);// you gain time for moving faster
    }

    private void PickUpCoin(Collider other)
    {
        Assert.IsTrue(other.gameObject.GetComponent<PickUp>().GetPickUp() == Type.coin);
        scoreManager.AddScore(other.gameObject.GetComponent<PickUp>().GetPickAmmount());
    }
}
