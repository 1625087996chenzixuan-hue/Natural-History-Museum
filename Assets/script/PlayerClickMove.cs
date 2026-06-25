using UnityEngine;
using UnityEngine.AI;

public class PlayerClickMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera cam;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) cam = Camera.main;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("🎯 点击到：" + hit.collider.name);

                // -------------------------
                // 1. 交互判断（不阻断移动）
                // -------------------------
                InteractableObject obj = hit.collider.GetComponentInParent<InteractableObject>();

                if (obj != null)
                {
                    Debug.Log("🟡 命中交互物：" + obj.name);

                    obj.OnClick(agent);

                    // ❗不 return，让逻辑继续
                }

                // -------------------------
                // 2. 移动逻辑（永远执行）
                // -------------------------
                if (hit.collider.CompareTag("Ground"))
                {
                    agent.SetDestination(hit.point);
                    Debug.Log("🚶 移动到：" + hit.point);
                }
            }
        }
    }
}