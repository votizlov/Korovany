using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Korovan : MonoBehaviour
{
    [SerializeField] private GameProxy gameProxy;

    private void OnDestroy()
    {
        gameProxy.gameManager.OnKorovanDestroyed();
    }
}