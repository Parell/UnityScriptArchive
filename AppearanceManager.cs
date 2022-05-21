using UnityEngine;

public class AppearanceManager : MonoBehaviour
{
    public Appearance[] appearances;

    [Space]
    public GameObject[] heads;

    public Material[] skinsColors;
    public GameObject[] hairs;
    public Material[] hairColors;
    public GameObject[] eyes;
    public GameObject[] eyeColors;
    public GameObject[] eyebrows;
    public GameObject[] noses;
    public GameObject[] mouths;
    public GameObject[] cloths;

    [Range(1, 10)]
    public int height;

    [Range(1, 10)]
    public int muscle;

    [Range(1, 10)]
    public int fat;

    private void Awake()
    {
        LoadCharacter();
    }

    public void LoadCharacter()
    {
        //heads[appearances[2].value].gameObject.SetActive(true);
    }
}

[System.Serializable]
public class Appearance
{
    [System.NonSerialized]
    public AppearanceManager parent;

    public Appearance type;
    public string Name;
    public int value;
}