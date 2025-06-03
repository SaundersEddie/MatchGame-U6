using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform tileParent; // GridLayoutGroup container
    public List<Sprite> allTileSprites;

    private List<TileButton> activeTiles = new List<TileButton>();
    private List<Sprite> selectedSprites = new List<Sprite>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        selectedSprites.Clear();

        List<Sprite> pool = new List<Sprite>(allTileSprites);

        while (selectedSprites.Count < 18)
        {
            int index = Random.Range(0, pool.Count);
            selectedSprites.Add(pool[index]);
            pool.RemoveAt(index);
        }

        List<Sprite> gameSprites = new List<Sprite>();
        foreach (Sprite sprite in selectedSprites)
        {
            gameSprites.Add(sprite);
            gameSprites.Add(sprite);
        }

        // Shuffle
        for (int i = 0; i < gameSprites.Count; i++)
        {
            Sprite temp = gameSprites[i];
            int rand = Random.Range(i, gameSprites.Count);
            gameSprites[i] = gameSprites[rand];
            gameSprites[rand] = temp;
        }

        // Create grid
        foreach (Sprite sprite in gameSprites)
        {
            GameObject tile = Instantiate(tilePrefab, tileParent);
            TileButton tileButton = tile.GetComponent<TileButton>();
            tileButton.Init(sprite, this);
        }
    }

    public void TileSelected(TileButton tile)
    {
        if (activeTiles.Count >= 2 || activeTiles.Contains(tile)) return;

        activeTiles.Add(tile);

        if (activeTiles.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    System.Collections.IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        TileButton tile1 = activeTiles[0];
        TileButton tile2 = activeTiles[1];

        if (tile1.GetSprite() == tile2.GetSprite())
        {
            Debug.Log("Match Success!");

            Destroy(tile1.gameObject);
            Destroy(tile2.gameObject);
        }
        else
        {
            Debug.Log("Match Failed");

            tile1.ResetTile();
            tile2.ResetTile();
        }

        activeTiles.Clear();
    }
}
