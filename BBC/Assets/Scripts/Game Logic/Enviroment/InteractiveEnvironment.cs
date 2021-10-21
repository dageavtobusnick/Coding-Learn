using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEnvironment : MonoBehaviour
{
    public string openAnimationName;
    public float openAnimationTime;
    public string closeAnimationName;
    public float closeAnimationTime;

    private GameManager gameManager;
    private bool isPlayerClose;
    private bool isAnimationStarted = false;
    private bool isOpenAnimation = true;

    private IEnumerator PlayAnimation_COR(string animationName, float latency)
    {
        isAnimationStarted = true;
        GetComponentInParent<Animator>().Play(animationName);
        yield return new WaitForSeconds(latency);
        isAnimationStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameManager.Player)
        {
            GetComponent<InteractiveItemMarker>().enabled = true;
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameManager.Player)
        {
            GetComponent<InteractiveItemMarker>().enabled = false;
            isPlayerClose = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose && !isAnimationStarted)
        {
            if (isOpenAnimation)
                StartCoroutine(PlayAnimation_COR(openAnimationName, openAnimationTime));
            else StartCoroutine(PlayAnimation_COR(closeAnimationName, closeAnimationTime));
            isOpenAnimation = !isOpenAnimation;
        }

    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
}
