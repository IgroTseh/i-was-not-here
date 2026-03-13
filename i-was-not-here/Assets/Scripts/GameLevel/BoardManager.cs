using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Tilemap exitTileMap;
    [SerializeField] private Tilemap tileMap;

    [SerializeField] private Tile[] floorTiles;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile exitTile;
    [SerializeField] private GameObject popaPrefab;
    [SerializeField] private GameObject nepotrebstvoPrefab;

    [SerializeField] private int maxCells = 15;

    

    private Grid grid;
    private List<Vector2Int> neighbourCellsCoords;
    private List<Vector2Int> wallsCoords;
    private List<Vector2Int> floorCoords;

    public void Init()
    {
        grid = GetComponentInChildren<Grid>();
        neighbourCellsCoords = new List<Vector2Int>();
        wallsCoords = new List<Vector2Int>();
        floorCoords = new List<Vector2Int>();

        GenerateField();
        GenerateWalls();
        GenerateExit();
        GenerateMobs();
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

            SetCellTile(tileMap, coord, tile);
            AddNeighbourCellsCoords(neighbourCellsCoords, coord);
            floorCoords.Add(coord);
        }
    }

    private void GenerateWalls()
    {
        for (int i = neighbourCellsCoords.Count - 1; i >= 0; i--)
        {
            var candidate = neighbourCellsCoords[i];

            if ((!tileMap.HasTile(new Vector3Int(candidate.x, candidate.y, 0))))
            {
                SetCellTile(tileMap, candidate, wallTile);
                wallsCoords.Add(candidate);
            }
        }
    }

    private void GenerateExit()
    {
        int randomWallIndex = UnityEngine.Random.Range(0, wallsCoords.Count);
        Vector2Int randomWallCoords = wallsCoords[randomWallIndex];

        SetCellTile(tileMap, randomWallCoords, floorTiles[0]);
        SetCellTile(exitTileMap, randomWallCoords, exitTile);
    }

    private void GenerateMobs()
    {
        int cntPopas = GetSpawnCount();
        int cntNepotrebstvas = GetSpawnCount() * 3;

        SpawnMobType(popaPrefab, cntPopas);
        SpawnMobType(nepotrebstvoPrefab, cntNepotrebstvas);
    }

    private int GetSpawnCount()
    {
        int level = GameManager.Instance.CurrLevel;
        float levelCoeff = GameManager.Instance.CoeffLevel;

        int maxCount = Convert.ToInt32(Math.Round(3 + level * levelCoeff));
        return UnityEngine.Random.Range(1, maxCount);
    }

    public void SpawnMobType(GameObject mob, int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            int randomFloorTileCoord = UnityEngine.Random.Range(0, floorCoords.Count);
            SpawnInstance(mob, floorCoords[randomFloorTileCoord]);
        }
    }

    private void SpawnInstance(GameObject prefab, Vector2Int coords)
    {
        Vector3 spawnCoords = new Vector3(coords.x, coords.y, -10);
        Quaternion spawnQua = Quaternion.identity;

        Instantiate(prefab, spawnCoords, spawnQua);
    }

    private void SetCellTile(Tilemap tileM, Vector2Int coord, Tile tile)
    {
        tileM.SetTile(new Vector3Int(coord.x, coord.y, 0), tile);
    }


    private void AddNeighbourCellsCoords(List<Vector2Int> neighbours, Vector2Int coord)
    {
        neighbours.Add(new Vector2Int(coord.x + 1, coord.y));
        neighbours.Add(new Vector2Int(coord.x - 1, coord.y));
        neighbours.Add(new Vector2Int(coord.x, coord.y + 1));
        neighbours.Add(new Vector2Int(coord.x, coord.y - 1));

    }


}
