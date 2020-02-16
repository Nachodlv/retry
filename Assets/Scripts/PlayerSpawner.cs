using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerSpawner : MonoBehaviour
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
    
    private PlayerController[] ships;
    private int currentLife;
    private PlayerController currentShip;

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

        ships = new PlayerController[lives];
        for (int i = 0; i < ships.Length; i++)
        {
            var ship = Instantiate(player).GetComponent<PlayerController>();
            ship.enabled = false;
            ship.SetTouchController(touchController);
            ship.gameObject.SetActive(false);
            ship.gameObject.GetComponent<Stats>().OnDie += () => OnShipDie(ship);
            ships[i] = ship;
        }

        InitializeShip();
    }

    /// <summary>
    /// <para>Disables the PlayerController of the currentShip and sets the currentShip to the next available ship</para>
    /// </summary>
    private void InitializeShip()
    {
        if (currentShip != null)
        {
            currentShip.enabled = false;
        }

        currentShip= ships[currentLife];
        currentShip.gameObject.SetActive(true);
        currentShip.enabled = true;
    }

    /// <summary>
    /// <para>Initializes the next available ship if there is any and invokes the OnShipDestroyed event.</para>
    /// <para>If there is no ships available then the OnAllShipsDestroyed event will be invoked</para>
    /// <remarks>This method is executed when a player ship is destroyed</remarks>
    /// </summary>
    /// <param name="ship"></param>
    private void OnShipDie(PlayerController ship)
    {
        if (ship != ships[currentLife]) return;
        
        currentLife++;
        livesUI.UpdateValue(lives - currentLife);
        if (currentLife >= lives) OnAllShipsDestroyed?.Invoke();
        else
        {
            OnShipDestroyed?.Invoke();
            InitializeShip();
        }
    }
}