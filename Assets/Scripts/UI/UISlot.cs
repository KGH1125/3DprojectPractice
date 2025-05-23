using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.addItem += AddItem;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

    }
}
