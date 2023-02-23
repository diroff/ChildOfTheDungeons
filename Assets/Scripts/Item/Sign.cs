using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [Header("Direction count")]
    [SerializeField] private int _maximumDirectionCount = 3;
    [SerializeField] private int _minimumDirectionCount = 2;

    [Header("Directions")]
    [SerializeField] private List<Direction> _directions;

    public int MaximumDirectionCount => _maximumDirectionCount;
    public int MinimumDirectionCount => _minimumDirectionCount;

    public List<Direction> Directions => _directions;
}
