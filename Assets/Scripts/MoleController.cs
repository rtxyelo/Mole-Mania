using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoleController : MonoBehaviour
{
    [SerializeField]
    private Button _beatButton;

    [SerializeField]
    private BeatSoundBehaviour _beatSoundBehaviour;

    private MoleUpDownAnimation _moleUpDownAnimation;

    [SerializeField]
    private Animator _moleAnimator;

    private bool _isBeat = false;

    private bool _isDown = false;

    public UnityEvent MoleIsBeat;

    private void Awake()
    {
        _moleUpDownAnimation = GetComponent<MoleUpDownAnimation>();
        _moleUpDownAnimation.OnMoleAheadComplete.AddListener(MoleDown);

        _beatButton.enabled = false;
    }

    private void Start()
    {
        _moleAnimator.SetBool("IsBeat", _isBeat);
        _moleAnimator.SetBool("IsDown", _isDown);
    }

    public void SpawnMole()
    {
        _isDown = false;
        _moleAnimator.SetBool("IsDown", _isDown);

        _isBeat = false;
        _moleAnimator.SetBool("IsBeat", _isBeat);
        
        _moleUpDownAnimation.MoleAhead();
        
        _moleAnimator.Play("Up");
        
        _beatButton.enabled = true;
    }

    public void BeatButtonPressed()
    {
        //Debug.Log("Mole is beat!");

        _beatSoundBehaviour.PlayBeatSound();

        _moleUpDownAnimation.MoleIsBeat();

        _beatButton.enabled = false;

        _isBeat = true;
        _moleAnimator.SetBool("IsBeat", _isBeat);

        MoleIsBeat.Invoke();
    }

    public void SetIsBeatFlag(int beatFlag)
    {
        _isBeat = Convert.ToBoolean(beatFlag);
        _moleAnimator.SetBool("IsBeat", _isBeat);

        _moleUpDownAnimation.OnInitialPosition();
    }

    private void MoleDown()
    {
        //Debug.Log("Mole without beat!");

        _beatButton.enabled = false;

        _isDown = true;
        _moleAnimator.SetBool("IsDown", _isDown);
    }
}
