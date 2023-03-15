using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar :Bar
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        Slider.value = 0;
        _spawner.EnemyCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemyCountChanged -= OnValueChanged;
    }
}
