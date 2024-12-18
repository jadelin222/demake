using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidSystem : MonoBehaviour
{
    public static PolaroidSystem Instance;

    [SerializeField] 
    private List<PolaroidData> polaroidCollection = new List<PolaroidData>();

    private void Awake()
    {
        Instance = this;
    }
    public void CollectPolaroid(Sprite image, string description)
    {
        PolaroidData newPolaroid = new PolaroidData(image, description);
        polaroidCollection.Add(newPolaroid);
        //UIManager.Instance.SetLastCollectedCategory(InventoryCategory.Polaroids);
    }
    public List<PolaroidData> GetPolaroidCollection()
    {
        return polaroidCollection;
    }

    [ContextMenu("print Polaroid inventory")]
    private void DebugInventory()
    {
        foreach (var polaroid in polaroidCollection)
        {
            Debug.Log($"Polaroid: {polaroid.descriptionText}, Solved: {polaroid.isPuzzleSolved}");
        }
    }

public bool IsPuzzleSolved()
    {
        foreach (var polaroid in polaroidCollection)
        {
            //if xxxxxx
            return polaroid.isPuzzleSolved;
            
        }
        return false;
    }
    public void MarkPuzzleSolved(string polaroidName)
    {
        
    }
    
}
