using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Put me at the bottom of the Hierarchy
 * so that I am on top of everything else.
*/

public class Fader : MonoBehaviour
{
    private float _alpha;
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _fadeTime = 1.0f;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if( _canvasGroup == null )
        {
            Debug.LogError("Canvas Group is Null!");
        }

        /*
         * This causes an automatic fade-in when
         * the scene is loaded.
        */
        _canvasGroup.alpha = 1.0f;
        FadeIn();
    }

    public void FadeOut()
    {
        _alpha = _canvasGroup.alpha;
        StartCoroutine("StartFadeOut");
    }

    public void FadeIn()
    {
        _alpha = _canvasGroup.alpha;
        StartCoroutine("StartFadeIn");
    }

    private IEnumerator StartFadeOut()
    {
        while(_alpha < 1.0f)
        {
            _alpha += Time.deltaTime/_fadeTime;
            _canvasGroup.alpha = _alpha;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private IEnumerator StartFadeIn()
    {
        while (_alpha > 0)
        {
            _alpha -= Time.deltaTime/_fadeTime;
            _canvasGroup.alpha = _alpha;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
