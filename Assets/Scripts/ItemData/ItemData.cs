using System;
using UnityEngine;

public enum ItemType
{
    Equip, // ����
    Consumable // �Ҹ�
}

public enum StatType
{
    Health, // ü��
    Stamina, // ���׹̳�
    Speed, // �̵��ӵ�
    Jump // ������
}

public enum EffectApplicationType
{
    Instant, // ��� ����
    Duration // ���� �ð� ���� ����
}

[Serializable]
public class ItemEffect
{
    public StatType statType; // � �ɷ�ġ�� ��������
    public float value;       // ����� ��
    public EffectApplicationType applicationType; // ���,����
    public float duration; //���ӽð�
}

[CreateAssetMenu(fileName = "item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; // �̸�
    public string description; // ����
    public ItemType type;     // �Ҹ� / ����
    public Sprite icon;       // ������
    public GameObject dropPrefab; // ������

    [Header("Stacking")]
    public bool canStack;       // ��ø ���� ����
    public int maxStackAmount;  // ��ø ������ �ִ� ����

    [Header("Effects")]
    public ItemEffect[] itemEffects; // ������ ȿ��
}
