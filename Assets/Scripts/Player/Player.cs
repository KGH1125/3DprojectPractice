using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerCondition condition;
    public PlayerController controller;

    public ItemData itemData;
    public Action addItem;
    public Transform dropPosition;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        condition = GetComponent<PlayerCondition>();
        controller = GetComponent<PlayerController>();
    }
}
