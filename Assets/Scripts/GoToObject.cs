using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

public class GoToObject : Action
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float stoppingDistance = 2f; // Inspectorda 2 yaptığını belirttin
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
    }

    public override TaskStatus OnUpdate()
    {
        if (targetObject == null)
            return TaskStatus.Failure;

        targetPos = targetObject.position;
        
        // Yükseklik farklarını (Y eksenini) sıfırlayalım ki araba havaya/yere doğru gitmeye çalışmasın
        Vector3 currentPos = transform.position;
        targetPos.y = currentPos.y; 

        float distance = Vector3.Distance(currentPos, targetPos);

        // 1. HEDEFE ULAŞTI MI KONTROLÜ (En üstte olmalı ve ulaştıysa hemen bitirmeli)
        if (distance <= stoppingDistance)
        {
            return TaskStatus.Success; // Görev başarıyla biter, bir sonraki Wait görevine geçer.
        }

        Vector3 direction = (targetPos - currentPos).normalized;

        // 2. SADECE Y EKSENİNDE DÖNÜŞ (LookRotation arabalar için çok daha kararlıdır)
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }

        // 3. DOĞRU İLERİ HAREKET 
        // Kendi lokal ileri yönüne değil, hedefe doğru olan yön vektöründe hareket ettiriyoruz.
        transform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);

        return TaskStatus.Running;
    }

    public void SetTarget(Transform target)
    {
        targetObject = target;
    }
}