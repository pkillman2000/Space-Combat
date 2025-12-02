using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCloud : MonoBehaviour
{
    [SerializeField]
    private float _minSize = .15f;
    [SerializeField]
    private float _maxSize = .5f;
    [SerializeField]
    private float _minColorChangeTime = 2.0f;
    [SerializeField]
    private float _maxColorChangeTime = 5.0f;
    
    private Color _startLerpColor = new Color(.4150f, .1233f, .3926f, 1f);
    private Color _endLerpColor;
    private Color _currentLerpColor;
    private ParticleSystem _particleSystem;
    private ParticleSystem.ColorOverLifetimeModule _colorOverLifetime;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if ( _particleSystem != null )
        {
            // Create random size for cloud
            float scale = Random.Range(_minSize, _maxSize);
            transform.localScale = new Vector3(scale, scale, scale);

            // Set up color over lifetime
            _colorOverLifetime = _particleSystem.colorOverLifetime;
            _colorOverLifetime.enabled = true;
        }
        else
        {
            Debug.LogError("Gas Cloud script requires a particle system.");
            enabled = false;
        }

        _startLerpColor = MakeRandomColor();
        SetRandomColor(_startLerpColor);
        StartCoroutine(RandomColorChangeTimer());
    }

    IEnumerator RandomColorChangeTimer()
    {
        _endLerpColor = MakeRandomColor();
        float timer = 0f;
        float _duration = Random.Range(_minColorChangeTime, _maxColorChangeTime);
        while (true)
        {

            while (timer < _duration)
            {
                float t = timer / _duration;
                _currentLerpColor = Color.Lerp(_startLerpColor, _endLerpColor, t);
                SetRandomColor(_currentLerpColor);
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            _startLerpColor = _currentLerpColor;
            _endLerpColor = MakeRandomColor();
            yield return null;
        }
    }

    private Color MakeRandomColor()
    {
        // Create random color for cloud
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return(new Color(r, g, b, 1f));
    }

    private void SetRandomColor(Color color)
    {

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[4];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4];

        colorKeys[0].color = color;
        colorKeys[0].time = 0f;
        alphaKeys[0].alpha = 0f;
        alphaKeys[0].time = 0f;

        colorKeys[1].color = color;
        colorKeys[1].time = .25f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[1].time = .25f;

        colorKeys[2].color = color;
        colorKeys[2].time = .75f;
        alphaKeys[2].alpha = 1f;
        alphaKeys[2].time = .15f;

        colorKeys[3].color = color;
        colorKeys[3].time = 0f;
        alphaKeys[3].alpha = 0f;
        alphaKeys[3].time = 1f;

        gradient.SetKeys(colorKeys, alphaKeys);
        ParticleSystem.MinMaxGradient minMaxGradient = new ParticleSystem.MinMaxGradient(gradient);
        _colorOverLifetime.color = minMaxGradient;
    }
}
