using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadeImage;
    public float fadeSpeed = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void Teleport(string sceneName, string spawnName)
    {
        StartCoroutine(TeleportRoutine(sceneName, spawnName));
    }

    IEnumerator TeleportRoutine(string sceneName, string spawnName)
    {
        yield return FadeOut();

        SceneManager.LoadScene(sceneName);
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawn = GameObject.Find(spawnName);

        if (player != null && spawn != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

            if (agent != null)
                agent.enabled = false;

            player.transform.position = spawn.transform.position;

            if (agent != null)
            {
                agent.enabled = true;
                agent.Warp(spawn.transform.position);
            }
        }

        yield return FadeIn();
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }
}