using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Skills : MonoBehaviour
{
    [SerializeField] private Skill _power;
    [SerializeField] private Skill _agility;
    [SerializeField] private Skill _luck;
    [SerializeField] private Skill _endurance;

    public Skill Power => _power;
    public Skill Agility => _agility;
    public Skill Luck => _luck;
    public Skill Endurance => _endurance;
}