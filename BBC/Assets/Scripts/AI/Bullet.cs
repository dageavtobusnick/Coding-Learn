using EmeraldAI;
using EmeraldAI.Utility;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float CurrentDamage;
    public float Damage = 20;
    public bool DamageReduction = true;
    public float maxRandLimit = 5;
    public float minRandLimit = -5;
    public AnimationCurve PenetrationReductionGraph;

    private Vector3 PreviousStep;
    public bool RandomDamage = true;
    public int StartSpeed = 50;
    private float StartTime;
    public float TimeToDestruct = 10;
    private EmeraldAIProjectile projectile;
    public EmeraldAISystem AiSystem;
    
    private void Awake()
    {
        Invoke("DestroyNow", TimeToDestruct);

        var rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * StartSpeed);

        PreviousStep = gameObject.transform.position;

        StartTime = Time.time;

        CurrentDamage = Damage;
        if (RandomDamage)
            CurrentDamage += Random.Range(minRandLimit, maxRandLimit);
        projectile = gameObject.AddComponent<EmeraldAIProjectile>();
        projectile.EmeraldSystem = AiSystem;
        projectile.AbilityType = EmeraldAIProjectile.AbilityTypeEnum.Damage;
        projectile.IsBullet=true;
        rb.isKinematic=false;
        /*Keyframe[] ks;
        ks = new Keyframe [3];
       
        ks [0] = new Keyframe (0, 1);
        ks [1] = new Keyframe (StartPoinOfDamageReduction / 100, 1);
        ks [2] = new Keyframe (1, FinalDamageInPercent / 100);
       
        DamageReductionGraph = new AnimationCurve (ks);*/
    }

    private void FixedUpdate()
    {
        var CurrentStep = gameObject.transform.rotation;

        transform.LookAt(PreviousStep, transform.up);
        var hit = new RaycastHit();
        var Distance = Vector3.Distance(PreviousStep, transform.position);
        if (Distance == 0.0f)
            Distance = 1e-05f;
        Debug.Log(Distance);
        if (Physics.Raycast(PreviousStep, transform.TransformDirection(Vector3.back), out hit, Distance * 0.9999f) &&
            hit.transform.gameObject != gameObject)
        {
            Debug.Log("ok");
            RegisterHit(hit);
        }

        gameObject.transform.rotation = CurrentStep;

        PreviousStep = gameObject.transform.position;
    }

    private void RegisterHit(RaycastHit hit)
    {
        var playerHealth = GetComponent<EmeraldAIPlayerDamage>();
        if (playerHealth)
        {
            playerHealth.SendPlayerDamage(projectile.Damage,hit.transform,AiSystem);
        }
    }

    private void DestroyNow()
    {
        DestroyObject(gameObject);
    }

    private void SendDamage(GameObject Hit)
    {
        //Hit.SendMessage ("ApplyDamage", CurrentDamage * GetDamageCoefficient(), SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }

    private float GetDamageCoefficient()
    {
        var Value = 1.0f;
        var CurrentTime = Time.time - StartTime;
        Value = PenetrationReductionGraph.Evaluate(CurrentTime / TimeToDestruct);

        return Value;
    }
}