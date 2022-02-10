using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseManager : MonoBehaviour
{
    [SerializeField] public static bool isInside;
    private void Awake()
    {
        isInside = false;
    }
}
