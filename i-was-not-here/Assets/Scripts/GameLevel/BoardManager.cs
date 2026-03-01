using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField] Tile[] floorTiles;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile exitTile;

    [SerializeField] private int maxCells = 15;

    private Tilemap tileMap;
    private Grid grid;
    private List<Vector2Int> neighbourCellsCoords;


    public void Start()
    {

        tileMap = GetComponentInChildren<Tilemap>();
        grid = GetComponentInChildren<Grid>();
        neighbourCellsCoords = new List<Vector2Int>();

        GenerateField();

    }

    private void GenerateField()
    {

        for (int i = 0; i < maxCells; i++)
        {
            Vector2Int coord;
            Tile tile;

            if (i == 0)
            {
                coord = new Vector2Int(0, 0);
            }
            else
            {
                int randomNeighbourIndex = Random.Range(0, neighbourCellsCoords.Count);
                coord = neighbourCellsCoords[randomNeighbourIndex];
                neighbourCellsCoords.RemoveAt(randomNeighbourIndex);
            }

            int randomTileIndex = Random.Range(0, floorTiles.Length);
            tile = floorTiles[randomTileIndex];

            SetCellTile(coord, tile);
            AddNeighbourCellsCoords(neighbourCellsCoords, coord);
        }

        while (neighbourCellsCoords.Count > 0)
        {
            var candidate = neighbourCellsCoords[0];

            if ((!tileMap.HasTile(new Vector3Int(candidate.x, candidate.y, 0))))
                SetCellTile(candidate, wallTile);

            neighbourCellsCoords.RemoveAt(0);
        }
    }

    private void SetCellTile(Vector2Int coord, Tile tile)
    {
        tileMap.SetTile(new Vector3Int(coord.x, coord.y, 0), tile);
    }

    private void AddNeighbourCellsCoords(List<Vector2Int> neighbours, Vector2Int coord)
    {
        neighbours.Add(new Vector2Int(coord.x + 1, coord.y));
        neighbours.Add(new Vector2Int(coord.x - 1, coord.y));
        neighbours.Add(new Vector2Int(coord.x, coord.y + 1));
        neighbours.Add(new Vector2Int(coord.x, coord.y - 1));

    }


}
