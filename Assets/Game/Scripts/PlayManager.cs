using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayManager : MonoBehaviour
{
    private static PlayManager _instance = null;
    public static PlayManager instance => _instance;

    [SerializeField] private ColorBrickOS colorBrickData;
    [SerializeField] List<GameObject> listCharacter = new List<GameObject>();
    [SerializeField] GameObject surface;
    [SerializeField] CameraFollow mainCamera;
    [SerializeField] int _countCharacter = 3;

    private int countMap = 2;

    private bool _endGame = false;
    public bool endGame => _endGame;

    private GameObject _characterWin;
    public GameObject characterWin => _characterWin;

    private string _currentLevelIndex;
    public string currentLevelIndex => _currentLevelIndex;

    private GameObject map;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        }
        else if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void BakeNavMesh()
    {
       
        surface.GetComponent<NavMeshSurface>().BuildNavMesh();
    }


    public Material GetMaterial(ColorBrickEnum colorType)
    {
        return colorBrickData.list[(int)colorType];
    }

    public void SetCamera(GameObject player)
    {
        mainCamera.SetPlayer(player);
    }

    public void SpawnCharacter()
    {
        GameObject playerPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_CHARACTER}Player");
        GameObject player = Instantiate(playerPrefab);
        SetCamera(player);
        listCharacter.Add(player);
        for(int i = 0; i < _countCharacter - 1; i++)
        {
            GameObject enemyPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_CHARACTER}Enemy_{i+1}");
            GameObject enemy = Instantiate(enemyPrefab);
            listCharacter.Add(enemy);
        }
        SetCharacter();
    }

    public void SetCharacter()
    {
        List<int> listIndex = new List<int>();
        for (int i = 0; i < Enum.GetNames(typeof(ColorBrickEnum)).Length - 1; i++)
        {
            listIndex.Add(i);
        }
        foreach (GameObject character in listCharacter)
        {
            int randomIndex = Random.Range(0, listIndex.Count);
            character.GetComponent<Character>().SetColorCharacter((ColorBrickEnum)listIndex[randomIndex]);
            listIndex.RemoveAt(randomIndex);
        }
    }

    public void SetMap()
    {
        MapController.Instance.OnInit();

        foreach (GameObject character in listCharacter)
        {
            MapController.Instance.GetFloorByLevel(1).GetComponent<FloorController>().AutoInstantiateBrick((ColorBrickEnum)character.GetComponent<Character>().colorPlayer);
        }


    }

    public void SetEndGame(bool check)
    {
        this._endGame = check;
    }

    public void SetCharacterWin(GameObject characterWin)
    {
        this._characterWin = characterWin;
    }

    public void SetCurrentLevelIndex(string textIndex)
    {
        this._currentLevelIndex = textIndex;
        Debug.Log(PlayManager.instance.currentLevelIndex);
    }

    public void SpawnGameLevel(string textIndex)
    {
        GameObject mapPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_MAP}Map_{textIndex}");
        Debug.Log($"{PathConstants.PATH_MAP}Map_{textIndex}");
        map = Instantiate(mapPrefab);
        Invoke(nameof(BakeNavMesh), 0.2f);
        Invoke(nameof(SpawnCharacter), 0.3f);
        Invoke(nameof(SetMap), 0.5f);
        UIManager.Instance.OpenUI<UIGamePlay>();
        PlayerPrefs.SetString(KeyConstants.KEY_LEVEL_USER, textIndex);
    }

    [ContextMenu("DestroyGameLevel")]
    public void DestroyGameLevel()
    {
        Destroy(map);
        foreach (GameObject character in listCharacter)
        {
            Destroy(character);
        }
        listCharacter.Clear();
        _endGame = false;
    }

    public bool isNextMap()
    {
        return int.Parse(currentLevelIndex) < countMap;
    }
    
}

public enum ColorBrickEnum
{
    RED = 0,
    GREEN = 1,
    PURPLE = 2,
    GRAY = 3,
}
