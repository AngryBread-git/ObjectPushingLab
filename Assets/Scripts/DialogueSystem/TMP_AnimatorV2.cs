using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AnimationStyle
{
    none,
    shaking,
    wobbling,
    floating,
    waving,
}

public enum TextAnimationIntensity
{
    high,
    medium,
    low,
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_AnimatorV2 : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private TMP_TextInfo _textInfo;
    private int _characterCount;


    private Vector3[] _tempVertices;
    private Mesh _tempMesh;

    [SerializeField] private bool _listenForEvents = true;
    [SerializeField] private AnimationStyle _animationStyle;
    [SerializeField] private TextAnimationIntensity _animationIntensity;

    [SerializeField] private bool _animateSpecifiedWords;
    [SerializeField] private List<int> _specifiedWordIndexes;

    [SerializeField] private float _shakeHeightSpeed;
    [SerializeField] private float _shakeWidthSpeed;

    [SerializeField] private float _wobbleHeightSpeed;
    [SerializeField] private float _wobbleWidthSpeed;

    [SerializeField] private float _floatHeightSpeed;
    [SerializeField] private float _floatWidthSpeed;

    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveLength;
    [SerializeField] private float _waveHeight;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    //Uses LateUpdate to avoid any jitter/stutters when printing a textline.
    void LateUpdate()
    {
        //If there are no characters or the line is set to not animate, then don't do anything.
        if (_characterCount == 0 || _animationStyle == AnimationStyle.none) 
        {
            return;
        }

        //Debug.Log(string.Format("TMP animV2, _characterCount is: {0}", _characterCount));
        _textMesh.ForceMeshUpdate();
        _tempMesh = _textMesh.mesh;
        _tempVertices = _tempMesh.vertices;

        int currentWordIndex = 0;

        for (int i = 0; i < _characterCount; i++) 
        {
            //Debug.Log(string.Format("TMP animV2, i is: {0}", i));
            //Debug.Log(string.Format("TMP animV2, currentWordIndex is: {0}", currentWordIndex));
                       
            TMP_CharacterInfo charInfo = _textInfo.characterInfo[i];
            
            //Don't work on invisible characters. increase word index by 1 then return to start of for-loop
            //NOTE: This means that characters that are not present in the font are skipped. Be very careful with that.
            if (!charInfo.isVisible)
            {
                //if there is a space then it's the end of a word, so increase the word index.
                currentWordIndex += 1;
                continue;
            }


            if (_animateSpecifiedWords)
            {
                if (!_specifiedWordIndexes.Contains(currentWordIndex)) 
                {
                    continue;
                }
                
            }
            

            if (_animationStyle == AnimationStyle.shaking)
            {
                ApplyShakeMotion(i);
            }
            
            else if (_animationStyle == AnimationStyle.wobbling)
            {
                //Wobble moves the entire line as one entity, so it is only called once.
                ApplyWobbleMotion();             
                break;
            }


            else if (_animationStyle == AnimationStyle.floating)
            {
                ApplyFloatMotion(i, currentWordIndex);
            }


            else if (_animationStyle == AnimationStyle.waving)
            {
                ApplyWaveMotion(i);
            }
        }


        //Apply the modified vertices.
        _tempMesh.vertices = _tempVertices;
        _textMesh.canvasRenderer.SetMesh(_tempMesh);
    }

    //Move each letter seperatly
    private void ApplyShakeMotion(int characterIndex) 
    {
        TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[characterIndex];
        //Debug.Log(string.Format("TMP anim, _textMesh is: {0}", _textMesh.textInfo.));

        int index = c.vertexIndex;

        Vector3 offset = ShakeMotion(Time.time + characterIndex);

        //for every vertex in the letter
        for (int i = 0; i < 4; i++)
        {
            _tempVertices[index + i] += offset;
        }
    }

    //Move the entire line as one entity.
    private void ApplyWobbleMotion() 
    {
        //One offset for the entire row.
        Vector3 offset = WobbleMotion(Time.time);

        //for every vertex in the row
        for (int j = 0; j < _tempVertices.Length; j++)
        {            
            _tempVertices[j] += offset;
        }
    }

    //Move each word seperatly
    private void ApplyFloatMotion(int characterIndex, int wordIndex) 
    {
        //Debug.Log(string.Format("apply float motion, characterIndex is: {0}, wordIndex is: {1}", characterIndex, wordIndex));
        Vector3 offset = FloatMotion(Time.time + wordIndex);

        TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[characterIndex];

        int index = c.vertexIndex;

        //for every vertex in the letter
        for (int j = 0; j < 4; j++)
        {
            _tempVertices[index + j] += offset;
        }
    }

    //Move the entire line like a wave.
    private void ApplyWaveMotion(int characterIndex)
    {

        TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[characterIndex];
        int index = c.vertexIndex;

        //for every vertex in the letter
        for (int i = 0; i < 4; i++)
        {
            Vector3 offset = WaveMotion(_tempVertices[index + i]);
            _tempVertices[index + i] += offset;
        }
    }

    #region CalculateMotions

    private Vector2 ShakeMotion(float incrementedTime)
    {
        Vector2 result = new Vector2(Mathf.Sin(incrementedTime * _shakeHeightSpeed), Mathf.Cos(incrementedTime * _shakeWidthSpeed));

        return result;
    }


    private Vector2 WobbleMotion(float incrementedTime)
    {
        Vector2 result = new Vector2(Mathf.Sin(incrementedTime * _wobbleHeightSpeed), Mathf.Cos(incrementedTime * _wobbleWidthSpeed));

        return result;
    }

    private Vector2 FloatMotion(float incrementedTime)
    {
        Vector2 result = new Vector2(Mathf.Sin(incrementedTime * _floatHeightSpeed), Mathf.Cos(incrementedTime * _floatWidthSpeed));

        return result;
    }


    private Vector2 WaveMotion(Vector2 orgVector2)
    {
        //Debug.Log(string.Format("WaveMotion, orgVector2 is: {0}", orgVector2));

        Vector2 result = new Vector2(0, Mathf.Sin(Time.time * _waveSpeed + orgVector2.x * _waveLength) * _waveHeight);

        //Debug.Log(string.Format("WaveMotion, result is: {0}", result));
        return result;
    }

    #endregion CalculateMotions


    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChange);

        if (_listenForEvents)
        {
            EventCoordinator<SetTextAnimationStyleEventInfo>.RegisterListener(SetAnimationStyle);
            EventCoordinator<SetTextAnimationIntensityEventInfo>.RegisterListener(SetAnimationIntensity);

            EventCoordinator<SetSpecifiedWordAnimationEventInfo>.RegisterListener(SetSpecifiedWordAnimation);
        }
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChange);
        if (_listenForEvents)
        {
            EventCoordinator<SetTextAnimationStyleEventInfo>.UnregisterListener(SetAnimationStyle);
            EventCoordinator<SetTextAnimationIntensityEventInfo>.UnregisterListener(SetAnimationIntensity);

            EventCoordinator<SetSpecifiedWordAnimationEventInfo>.UnregisterListener(SetSpecifiedWordAnimation);

        }
    }

    #region EventListeners
    private void OnTextChange(Object obj) 
    {
        //Debug.Log(string.Format("OnTextChange: "));
        if (obj == _textMesh)
        {
            _textInfo = _textMesh.textInfo;
            _characterCount = _textMesh.textInfo.characterCount;
        }
        
    }

    private void SetAnimationStyle(SetTextAnimationStyleEventInfo ei) 
    {
        _animationStyle = ei._animationStyle;
    }


    private void SetAnimationIntensity(SetTextAnimationIntensityEventInfo ei)
    {
        _animationIntensity = ei._textAnimationIntensity;
        ApplyAnimationIntensity();
    }

    //setters for the different values.
    private void ApplyAnimationIntensity()
    {
        //Apply the coded values to each variable based on intensity.
        if (_animationIntensity == TextAnimationIntensity.high)
        {
            _shakeHeightSpeed = 2.8f;
            _shakeWidthSpeed = 2.5f;

            _wobbleHeightSpeed = 2.6f;
            _wobbleWidthSpeed = 2.2f;

            _floatHeightSpeed = 1.7f;
            _floatWidthSpeed = 1.3f;

            _waveSpeed = 1.9f;
            _waveLength = 0.01f;
            _waveHeight = 4.5f;
        }
        else if (_animationIntensity == TextAnimationIntensity.medium)
        {
            _shakeHeightSpeed = 2.4f;
            _shakeWidthSpeed = 2.0f;

            _wobbleHeightSpeed = 2.2f;
            _wobbleWidthSpeed = 1.8f;

            _floatHeightSpeed = 1.5f;
            _floatWidthSpeed = 1.2f;

            _waveSpeed = 1.4f;
            _waveLength = 0.01f;
            _waveHeight = 4.0f;
        }

        else if (_animationIntensity == TextAnimationIntensity.low)
        {
            _shakeHeightSpeed = 2.0f;
            _shakeWidthSpeed = 1.5f;

            _wobbleHeightSpeed = 1.8f;
            _wobbleWidthSpeed = 1.6f;

            _floatHeightSpeed = 1.3f;
            _floatWidthSpeed = 1.1f;

            _waveSpeed = 1.2f;
            _waveLength = 0.01f;
            _waveHeight = 3.5f;
        }

    }

    private void SetSpecifiedWordAnimation(SetSpecifiedWordAnimationEventInfo ei) 
    {
        _animateSpecifiedWords = ei._animatedOnlyOneWord;
        _specifiedWordIndexes = ei._specifiedWordIndexes;
    }



    #endregion EventListeners
}
