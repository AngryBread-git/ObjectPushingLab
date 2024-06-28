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

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_AnimatorV2 : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private TMP_TextInfo _textInfo;
    private int _characterCount;


    private Vector3[] _tempVertices;
    private Mesh _tempMesh;

    [SerializeField] private bool _listenForEvents;
    [SerializeField] private AnimationStyle _animationStyle;

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


    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChange);

        if (_listenForEvents) 
        {
            EventCoordinator<SetTextAnimationStyleEventInfo>.RegisterListener(SetAnimationStyle);
            EventCoordinator<SetSpecifiedWordAnimationEventInfo>.RegisterListener(SetSpecifiedWordAnimation);

            EventCoordinator<SetTextShakeEventInfo>.RegisterListener(SetShakeValues);
            EventCoordinator<SetTextWobbleEventInfo>.RegisterListener(SetWobbleValues);
            EventCoordinator<SetTextFloatEventInfo>.RegisterListener(SetFloatValues);
            EventCoordinator<SetTextWaveEventInfo>.RegisterListener(SetWaveValues);
        }

    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChange);
        if (_listenForEvents)
        {
            EventCoordinator<SetTextAnimationStyleEventInfo>.UnregisterListener(SetAnimationStyle);
            EventCoordinator<SetSpecifiedWordAnimationEventInfo>.UnregisterListener(SetSpecifiedWordAnimation);

            EventCoordinator<SetTextShakeEventInfo>.UnregisterListener(SetShakeValues);
            EventCoordinator<SetTextWobbleEventInfo>.UnregisterListener(SetWobbleValues);
            EventCoordinator<SetTextFloatEventInfo>.UnregisterListener(SetFloatValues);
            EventCoordinator<SetTextWaveEventInfo>.UnregisterListener(SetWaveValues);
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

    private void SetSpecifiedWordAnimation(SetSpecifiedWordAnimationEventInfo ei) 
    {
        _animateSpecifiedWords = ei._animatedOnlyOneWord;
        _specifiedWordIndexes = ei._specifiedWordIndexes;
    }

    private void SetShakeValues(SetTextShakeEventInfo ei)
    {
        _shakeHeightSpeed = ei._shakeHeightSpeed;
        _shakeWidthSpeed = ei._shakeWidthSpeed;
    }

    private void SetWobbleValues(SetTextWobbleEventInfo ei)
    {
        _wobbleHeightSpeed = ei._wobbleHeightSpeed;
        _wobbleWidthSpeed = ei._wobbleWidthSpeed;
    }

    private void SetFloatValues(SetTextFloatEventInfo ei) 
    {
        _floatHeightSpeed = ei._floatHeightSpeed;
        _floatWidthSpeed = ei._floatWidthSpeed;
    }

    private void SetWaveValues(SetTextWaveEventInfo ei)
    {
        _waveSpeed = ei._waveSpeed;
        _waveLength = ei._waveLength;
        _waveHeight = ei._waveHeight;
    }

    //setters for the different values.

    #endregion EventListeners
}
