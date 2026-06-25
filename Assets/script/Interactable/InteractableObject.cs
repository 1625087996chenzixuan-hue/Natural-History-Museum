using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    public InteractionType type;

    [Header("NPC对话")]
    public GameObject dialogPanel;
    public Text dialogText;
    [TextArea]
    public string dialogContent = "你好，欢迎来到展馆。";

    [Header("视频/展品面板")]
    public GameObject videoPanel;

    [Header("UI自动关闭时间")]
    public float autoHideTime = 3f;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void OnClick(NavMeshAgent playerAgent)
    {
        Debug.Log("🔥 OnClick已执行：" + gameObject.name);
        Debug.Log("👉 当前类型：" + type);

        switch (type)
        {
            case InteractionType.NPC:
                if (dialogPanel != null)
                {
                    dialogPanel.SetActive(true);

                    if (dialogText != null)
                        dialogText.text = dialogContent;

                    StartCoroutine(HideAfterSeconds(dialogPanel));
                }
                else
                {
                    Debug.LogError("❌ dialogPanel 没绑定！");
                }

                break;

            case InteractionType.Video:
                if (videoPanel != null)
                {
                    videoPanel.SetActive(true);

                    if (videoPlayer != null)
                    {
                        videoPlayer.Stop();
                        videoPlayer.Play();
                    }
                    else
                    {
                        Debug.LogError("❌ 当前物体上没有 VideoPlayer 组件！");
                    }

                    StartCoroutine(HideAfterSeconds(videoPanel));
                }
                else
                {
                    Debug.LogError("❌ videoPanel 没绑定！");
                }

                break;

            case InteractionType.Pickup:
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator HideAfterSeconds(GameObject panel)
    {
        yield return new WaitForSeconds(autoHideTime);

        if (type == InteractionType.Video && videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}