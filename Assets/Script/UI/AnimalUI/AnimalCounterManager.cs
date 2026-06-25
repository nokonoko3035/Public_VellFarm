using UnityEngine;

public class AnimalCounterManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimalCountData
    {
        public string tagName;
        [HideInInspector] public int count;
    }

    [Header("カウント対象の動物")]
    public AnimalCountData[] animalTypes;

    void Update()
    {
        foreach (var animal in animalTypes)
        {
            animal.count = GameObject.FindGameObjectsWithTag(animal.tagName).Length;
        }
    }

    /// <summary>
    /// 外部スクリプトから、タグで現在の数を取得できる
    /// </summary>
    public int GetCountByTag(string tag)
    {
        foreach (var animal in animalTypes)
        {
            if (animal.tagName == tag)
                return animal.count;
        }
        return 0;
    }
}
