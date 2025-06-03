using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    public Image tileImage;
    public Button button;
    public Sprite frontSprite;

    private GameManager gameManager;
    private bool isMatched = false;

    public void Init(Sprite sprite, GameManager manager)
    {
        frontSprite = sprite;
        tileImage.sprite = frontSprite;
        gameManager = manager;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (!isMatched)
        {
            gameManager.TileSelected(this);
        }
    }

    public void SetMatched()
    {
        isMatched = true;
        button.interactable = false;
    }

    public Sprite GetSprite()
    {
        return frontSprite;
    }

    public void ResetTile()
    {
        // Placeholder for future tile flip-back logic
    }

    public void Disable()
    {
        button.interactable = false;
    }
}
