using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

public class GoToObject : Action
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    
    private Vector3 targetPos;

    public override void OnStart()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target atanmadı!");
            return;
        }
        targetPos = targetObject.position;
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject == null)
            return TaskStatus.Failure;

        targetPos = targetObject.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPos);

        // Hedefe ulaştı mı?
        if (distance < stoppingDistance)
        {
            return TaskStatus.Success;
        }

        // Sadece Y ekseninde dön
        if (direction.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }

        // Ileri git
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        return TaskStatus.Running;
    }

    public void SetTarget(Transform target)
    {
        targetObject = target;
    }
}