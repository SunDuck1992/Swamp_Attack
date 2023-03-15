using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangedSpread;

    private void Start()
    {
        _transitionRange += Random.Range(-_rangedSpread, _rangedSpread);
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
