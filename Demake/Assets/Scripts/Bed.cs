using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public ItemType RequiredItem => ItemType.None;
    public string InteractionVerb => "Sleep";

    private GameTime gameTime;
    private void Start()
    {
        gameTime = FindObjectOfType<GameTime>();
    }
    public void Interact()
    {
        if (UIManager.Instance.isCutsceneActive) return;
        UIManager.Instance.ShowDayCutScene(gameTime.GetDay());
        //set time to next day 6am
        gameTime.SetTimeToNextDay(6f / 24f);
    }

    public void OnRayHit()
    {
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }
}
