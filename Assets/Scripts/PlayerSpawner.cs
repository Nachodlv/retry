using System;
using Object = UnityEngine.Object;

public class PlayerSpawner
{
    public event Action OnAllShipsDestroyed;

    private SimpleTouchController touchController;
    private PlayerController[] ships;
    private int lives;
    private int currentLife;
    private PlayerController player;
    private PlayerController currentShip;

    public PlayerSpawner(int lives, PlayerController player, SimpleTouchController touchController)
    {
        this.lives = lives;
        this.player = player;
        this.touchController = touchController;
    }
    
    public void InstantiatePlayerShips()
    {
        if(lives <= 0) return;
        
        ships = new PlayerController[lives];
        for (int i = 0; i < ships.Length; i++)
        {
            var ship = Object.Instantiate(player).GetComponent<PlayerController>();
            ship.enabled = false;
            ship.SetTouchController(touchController);
            ship.gameObject.SetActive(false);
            ship.gameObject.GetComponent<Stats>().OnDie += () => OnShipDie(ship);
            ships[i] = ship;
        }
        
        InitializeShip();
    }
    
    private void InitializeShip()
    {
        if (currentShip != null)
        {
            currentShip.enabled = false;
        }
        var ship = ships[currentLife];
        ship.gameObject.SetActive(true);
        ship.enabled = true;
    }

    private void OnShipDie(PlayerController ship)
    {
        if(ship != ships[currentLife]) return;

        currentLife++;
        if(currentLife >= lives) OnAllShipsDestroyed?.Invoke();
        else InitializeShip();
    }
}
