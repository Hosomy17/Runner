using UnityEngine;
using GamepadFramework;
using ScriptFramework;
using ManagerFrameWork;
using UnityEngine.UI;

public class GameScript : ScriptGeneric
{

    public static GameScript Instance { get; private set; }

    public GameObject cube;
    public GameObject getCoinParticles;
    public GameObject getGoldCoinParticles;
    public float speed;
    private SpawnerScript spawer;

    private bool turnAI;
    public Image buttonAI;

    public Text hudPoints;
    private int countPoints;
    private int buffCountPoints;

    void Awake()
    {
        if (Instance)
            Destroy(gameObject); 
        else
            Instance = this;

        countPoints = 0;
        buffCountPoints = 0;
    }

    void FixedUpdate()
    {
        if(buffCountPoints > 0)
        {
            buffCountPoints--;
            countPoints++;
        }
            
        hudPoints.text = "x" + countPoints.ToString();
    }

    void Start()
    {
        var gamepad = GetComponent<GamepadGeneric>();
        var cubeController = new CubeController();
        gamepad.controller = cubeController;
        cubeController.TrackObject(cube);

        spawer = GetComponent<SpawnerScript>();

        turnAI = false;

        InvokeRepeating("CreateItem", 0, 0.5f);
    }

    public void TurnAI()
    {
        turnAI = !turnAI;

        var cubeAI = GetComponent<CubeAI>();
        var gamepad = GetComponent<GamepadGeneric>();

        if(turnAI)
            cubeAI.SetAI(gamepad.controller);

        cubeAI.enabled = turnAI;
        gamepad.enabled = !turnAI;

        buttonAI.color = (turnAI) ? Color.green : Color.red;
    }

    public void Point(GameObject obj)
    {
        var particles = ObjectPool.Instance.GetObject(getCoinParticles);

        particles.transform.position = obj.transform.position;
        particles.GetComponent<ParticlesScript>().Play();

        buffCountPoints += 1;

        SoundManager.Instance.Play("SFX/Point");

        ObjectPool.Instance.ReturnObject(obj);
    }

    public void BigPoint(GameObject obj)
    {
        var particles = ObjectPool.Instance.GetObject(getGoldCoinParticles);

        particles.transform.position = obj.transform.position;
        particles.GetComponent<ParticlesScript>().Play();

        buffCountPoints += 10;

        SoundManager.Instance.Play("SFX/BigPoint");
        
        ObjectPool.Instance.ReturnObject(obj);
    }

    public void Damage(GameObject obj)
    {
        ObjectPool.Instance.ReturnObject(obj);
        Time.timeScale = 0;

        SoundManager.Instance.Play("SFX/Lost");
        SoundManager.Instance.Play("BGM/Game Over");

        hudPoints.text ="x"+(countPoints + buffCountPoints).ToString();

        GetComponent<MenuScript>().OpenCanvas();
    }

    private void CreateItem()
    {
        var item = spawer.Spawn();
        BehaviourPhysics.Move(item, Vector3.back, speed);
    }
}