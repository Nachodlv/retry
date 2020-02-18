using System;
using UnityEngine;

/// <summary>
/// <para>Stores the PlayerRecorder, PlayerController and Rigidbody2D of a Player ship</para>
/// </summary>
struct PlayerShip
{
    private PlayerRecorder playerRecorder;
    private PlayerController playerController;
    private Rigidbody2D rigidbody2D;

    public PlayerRecorder PlayerRecorder => playerRecorder;
    public PlayerController PlayerController => playerController;
    public Rigidbody2D Rigidbody2D => rigidbody2D;

    public PlayerShip(PlayerRecorder playerRecorder, PlayerController playerController, Rigidbody2D rigidbody2D)
    {
        this.playerRecorder = playerRecorder;
        this.playerController = playerController;
        this.rigidbody2D = rigidbody2D;
    }
}

/// <summary>
/// <para>Spawns the player ships. One ship is always controlled by the PlayerController and the ones destroyed will
/// be controlled by the PlayerRecorder</para>
/// </summary>
public class PlayerSpawner: MonoBehaviour
{
    public event Action OnAllShipsDestroyed;
    public event Action OnShipDestroyed;

    [SerializeField] [Tooltip("Touch controller to move the player ship")]
    private SimpleTouchController touchController;

    [SerializeField] [Tooltip("Player prefab")]
    private PlayerController player;

    [SerializeField] [Tooltip("Quantity of lives the player has")]
    private int lives = 2;

    [SerializeField] [Tooltip("Display of lives")]
    private ShowNumberUI livesUI;

    [SerializeField] [Tooltip("Initial position of the ship")]
    private Vector2 initialPosition;

    private PlayerShip[] ships;
    private int currentLife;
    private PlayerShip currentShip;

    /// <summary>
    /// <para>Restore lives and reset the previous player action records</para>
    /// </summary>
    public void NextLevel()
    {
        livesUI.UpdateValue(lives);
        ships[currentLife] = ships[0];
        ships[0] = currentShip;
        currentShip.PlayerRecorder.StartRecording();
        currentLife = 0;
    }
    
    private void Awake()
    {
        livesUI.UpdateValue(lives);
        InstantiatePlayerShips();
    }

    /// <summary>
    /// <para>Instantiates as many ships as lives. The PlayerController of each ship is disabled</para>
    /// <para>Initializes the first ship</para>
    /// <remarks>Heavy memory allocation!</remarks>
    /// </summary>
    private void InstantiatePlayerShips()
    {
        if (lives <= 0) return;

        ships = new PlayerShip[lives];
        for (int i = 0; i < ships.Length; i++)
        {
            var playerController = Instantiate(player);
            var playerRecorder = playerController.GetComponent<PlayerRecorder>();

            var ship = new PlayerShip(playerRecorder, playerController, playerController.GetComponent<Rigidbody2D>());

            playerController.Enable = false;
            playerController.SetTouchController(touchController);
            playerController.gameObject.SetActive(false);
            playerController.gameObject.GetComponent<Stats>().OnDie += () => OnShipDie(ship);
            ships[i] = ship;
        }

        InitializeShip();
    }

    /// <summary>
    /// <para>Disables the PlayerController of the currentShip and sets the currentShip to the next available ship</para>
    /// </summary>
    private void InitializeShip()
    {
        if (currentShip.PlayerController != null)
        {
            currentShip.PlayerController.Enable = false;
            currentShip.PlayerRecorder.StopRecording();
        }

        currentShip = ships[currentLife];
        currentShip.Rigidbody2D.velocity = Vector2.zero;
        currentShip.PlayerRecorder.StartRecording();
        currentShip.PlayerRecorder.gameObject.SetActive(true);
        currentShip.PlayerController.Enable = true;
        currentShip.PlayerController.transform.position = initialPosition;
        ReplayPreviousShips();
    }

    /// <summary>
    /// <para>Initializes the next available ship if there is any and invokes the OnShipDestroyed event.</para>
    /// <para>If there is no ships available then the OnAllShipsDestroyed event will be invoked</para>
    /// <remarks>This method is executed when a player ship is destroyed</remarks>
    /// </summary>
    /// <param name="ship"></param>
    private void OnShipDie(PlayerShip ship)
    {
        if (ship.PlayerController != ships[currentLife].PlayerController) return;

        currentLife++;
        livesUI.UpdateValue(lives - currentLife);
        if (currentLife >= lives) OnAllShipsDestroyed?.Invoke();
        else
        {
            InitializeShip();
            OnShipDestroyed?.Invoke();
        }
    }

    /// <summary>
    /// <para>Reproduces the recording of the ships previously destroyed</para>
    /// </summary>
    private void ReplayPreviousShips()
    {
        for (var i = 0; i < currentLife; i++)
        {
            var playerRecorder = ships[i].PlayerRecorder;
            var shipGameObject = playerRecorder.gameObject;
            shipGameObject.SetActive(true);
            shipGameObject.transform.position = initialPosition;
            playerRecorder.ReproduceRecord();
        }
    }
}