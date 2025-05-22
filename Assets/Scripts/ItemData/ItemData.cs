using System;
using UnityEngine;

public enum ItemType
{
    Equip, // 장착
    Consumable // 소모
}

public enum StatType
{
    Health, // 체력
    Stamina, // 스테미나
    Speed, // 이동속도
    Jump // 점프력
}

public enum EffectApplicationType
{
    Instant, // 즉시 적용
    Duration // 지속 시간 동안 적용
}

[Serializable]
public class ItemEffect
{
    public StatType statType; // 어떤 능력치를 변경할지
    public float value;       // 변경될 값
    public EffectApplicationType applicationType; // 즉시,지속
    public float duration; //지속시간
}

[CreateAssetMenu(fileName = "item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; // 이름
    public string description; // 설명
    public ItemType type;     // 소모 / 장착
    public Sprite icon;       // 아이콘
    public GameObject dropPrefab; // 프리팹

    [Header("Stacking")]
    public bool canStack;       // 중첩 가능 여부
    public int maxStackAmount;  // 중첩 가능한 최대 개수

    [Header("Effects")]
    public ItemEffect[] itemEffects; // 아이템 효과
}
