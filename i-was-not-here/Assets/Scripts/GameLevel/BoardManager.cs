using System;
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
    [SerializeField] Exit exitPrefab;

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
        GenerateWalls();
        GenerateExit();
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
                int randomNeighbourIndex = UnityEngine.Random.Range(0, neighbourCellsCoords.Count);
                coord = neighbourCellsCoords[randomNeighbourIndex];
                neighbourCellsCoords.RemoveAt(randomNeighbourIndex);
            }

            int randomTileIndex = UnityEngine.Random.Range(0, floorTiles.Length);
            tile = floorTiles[randomTileIndex];

            SetCellTile(coord, tile);
            AddNeighbourCellsCoords(neighbourCellsCoords, coord);
        }
    }

    private void GenerateWalls()
    {
        for (int i = neighbourCellsCoords.Count - 1; i >= 0; i--)
        {
            var candidate = neighbourCellsCoords[i];

            if ((!tileMap.HasTile(new Vector3Int(candidate.x, candidate.y, 0))))
            {
                if (i == neighbourCellsCoords.Count - 1)
                {
                    Debug.Log("Vihod");
                    SetCellTile(candidate, exitTile);
                }
                else
                {
                    Debug.Log("Stena");
                    SetCellTile(candidate, wallTile);
                }
            }
        }


    }


    private void GenerateExit()
    {
        int randomNeighbourIndex = UnityEngine.Random.Range(0, neighbourCellsCoords.Count);
        Vector2Int randomNeighbourCoords = neighbourCellsCoords[randomNeighbourIndex];

        SetCellTile(randomNeighbourCoords, exitTile);

        Vector3 exitCoords = new Vector3(randomNeighbourCoords.x, randomNeighbourCoords.y, 10);
        Instantiate(exitPrefab, exitCoords, Quaternion.identity);

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
