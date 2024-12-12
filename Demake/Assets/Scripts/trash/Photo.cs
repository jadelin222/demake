using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour, IInteractable
{
    public int photoNum;
    public string photoName;
    public Sprite photoImg;         
    public bool isPuzzleSolved = false;
    public bool isCollected = false;

    public ItemType RequiredItem => ItemType.None;
    public string InteractionVerb => "Inspect";
    private PhotoSystem photoSystem;

    public void InitializePhoto(int num, string name, Sprite img, bool puzzleSolved, bool collected)
    {
        photoNum = num;
        photoName = name;
        photoImg = img;
        isPuzzleSolved = puzzleSolved;
        isCollected = collected;
    }

    private void Awake()
    {
        //initialize photo system
        photoSystem = FindObjectOfType<PhotoSystem>(); 
    }
    public void SolvePuzzle()
    {
        isPuzzleSolved = true;
        Debug.Log($"Puzzle solved for photo: {photoName}");
    }

    public void Interact()
    {
        Debug.Log($"interact with photo, display photo content: {photoName}");
        Destroy(gameObject);
        //TD: show photo ui >> press e again to collect photo
        //collect to the collcted list for inventory (collection system)
    }

    public void OnRayHit()
    {
        Debug.Log($"looking at photo: {photoName}");
    }
}
