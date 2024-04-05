using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GridBrushBase;
using UnityEngine.Events;

public class MoleUpDownAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _beatPositionObj;

    private Vector3 _beatPosition;

    private float _distance = 450f;

    private float _duration = 0.3f;

    private Vector3 _initialPosition;

    private bool _isBeat = false;

    public UnityEvent OnMoleAheadBegin;
    public UnityEvent OnMoleAheadComplete;

    private void Awake()
    {
        OnMoleAheadBegin = new UnityEvent();
        OnMoleAheadComplete = new UnityEvent();

        _beatPosition = _beatPositionObj.transform.localPosition;
        _initialPosition = transform.localPosition;
    }

    private void Start()
    {
        DOTween.Init();
    }

    public void MoleAhead()
    {
        _isBeat = false;

        if (OnMoleAheadBegin != null)
        {
            OnMoleAheadBegin.Invoke();
        }

        transform.localPosition = _initialPosition;

        transform.DOLocalMoveY(_initialPosition.y + _distance, _duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                if (!_isBeat)
                {
                    MoveDown();
                }
            });
    }

    private void MoveDown()
    {
        transform.DOLocalMoveY(_initialPosition.y, _duration)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                if (!_isBeat)
                {
                    MoleAheadComplete();
                }
            });
    }

    private void MoleAheadComplete()
    {
        if (OnMoleAheadComplete != null)
        {
            OnMoleAheadComplete.Invoke();
        }
    }

    public void MoleIsBeat()
    {
        _isBeat = true;
        DOTween.Kill(transform, false);
        DOTween.Clear();
        transform.localPosition = _beatPosition;
    }

    public void OnInitialPosition()
    {
        transform.localPosition = _initialPosition;
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform, true);
        DOTween.Clear();
    }
}
