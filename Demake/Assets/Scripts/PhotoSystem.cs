using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSystem : MonoBehaviour
{
    
    public List<Photo> allPhotos = new List<Photo>();
    public List<Photo> collectedPhotos = new List<Photo>();

    public GameObject photoPrefab;

    //add photo to collected photo list
    public void CollectPhoto(Photo photo)
    {
        if (!collectedPhotos.Contains(photo))
        {
            collectedPhotos.Add(photo);
            Debug.Log($"Collected photo: {photo.photoName}");
        }
    }

    public bool IsPuzzleSolved(int photoNum)
    {
        Photo photo = allPhotos.Find(p => p.photoNum == photoNum);
        if (photo != null)
        {
            return photo.isPuzzleSolved;
        }
        return false;
    }

    public void InitializePhotos()
    {
        Sprite sampleSprite = null;  

        // Initialize photo by instantiating prefabs
        GameObject photoObject1 = Instantiate(photoPrefab);
        Photo photo1 = photoObject1.GetComponent<Photo>();
        photo1.InitializePhoto(1, "Old House", sampleSprite, false, false);
        allPhotos.Add(photo1);

        GameObject photoObject2 = Instantiate(photoPrefab);
        Photo photo2 = photoObject2.GetComponent<Photo>();
        photo2.InitializePhoto(2, "Creepy Forest", sampleSprite, false, false);
        allPhotos.Add(photo2);

        Debug.Log("Initialized all photos");
    }
}
