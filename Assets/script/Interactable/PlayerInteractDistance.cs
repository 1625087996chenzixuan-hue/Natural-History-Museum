using UnityEngine;
using UnityEngine.AI;

public class PlayerInteractDistance : MonoBehaviour
{
    public float interactRange = 5f;

    private NavMeshAgent agent;
    private InteractableObject currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("❌ 玩家身上没有 NavMeshAgent！");
        }
    }

    void Update()
    {
        FindClosest();

        if (currentTarget != null)
        {
            if (UIManager.Instance != null)
                UIManager.Instance.ShowButton();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("✅ 按下E，准备交互：" + currentTarget.gameObject.name);
                currentTarget.OnClick(agent);
            }
        }
        else
        {
            if (UIManager.Instance != null)
                UIManager.Instance.HideButton();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogWarning("⚠ 按下E，但附近没有可交互物体");
            }
        }
    }

    void FindClosest()
    {
        InteractableObject[] objs = FindObjectsOfType<InteractableObject>();

        float minDist = Mathf.Infinity;
        InteractableObject closest = null;

        foreach (var obj in objs)
        {
            float dist;

            Collider col = GetUsableCollider(obj);

            if (col != null)
            {
                Vector3 closestPoint = col.ClosestPoint(transform.position);
                dist = Vector3.Distance(transform.position, closestPoint);
            }
            else
            {
                dist = Vector3.Distance(transform.position, obj.transform.position);
            }

            if (dist <= interactRange && dist < minDist)
            {
                minDist = dist;
                closest = obj;
            }
        }

        currentTarget = closest;
    }

    Collider GetUsableCollider(InteractableObject obj)
    {
        // 优先用物体自己身上的 BoxCollider
        BoxCollider box = obj.GetComponent<BoxCollider>();
        if (box != null) return box;

        // 再找子物体里的 BoxCollider
        box = obj.GetComponentInChildren<BoxCollider>();
        if (box != null) return box;

        // SphereCollider
        SphereCollider sphere = obj.GetComponent<SphereCollider>();
        if (sphere != null) return sphere;

        sphere = obj.GetComponentInChildren<SphereCollider>();
        if (sphere != null) return sphere;

        // CapsuleCollider
        CapsuleCollider capsule = obj.GetComponent<CapsuleCollider>();
        if (capsule != null) return capsule;

        capsule = obj.GetComponentInChildren<CapsuleCollider>();
        if (capsule != null) return capsule;

        // 最后才考虑 MeshCollider，而且必须是 Convex
        MeshCollider mesh = obj.GetComponent<MeshCollider>();
        if (mesh != null && mesh.convex) return mesh;

        mesh = obj.GetComponentInChildren<MeshCollider>();
        if (mesh != null && mesh.convex) return mesh;

        return null;
    }
}