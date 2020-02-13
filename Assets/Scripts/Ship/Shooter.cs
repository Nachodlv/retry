using UnityEngine;

/// <summary>
/// <para>Shoots bullets when the controller event is called</para>
/// </summary>
public class Shooter : MonoBehaviour
{
    [SerializeField] [Tooltip("Controller that will tell the shooter when to shoot")]
    private Controller controller;

    [SerializeField] [Tooltip("Bullet that will be shoot")]
    private Pooleable bullet;

    [SerializeField] [Tooltip("Time in seconds between each shot")]
    private float cadence = 1f;

    [SerializeField] [Tooltip("If nonStop is true it will not stop shooting")]
    private bool nonStop = false;
    
    private ObjectPooler _objectPooler;
    private bool _canShoot;
    private float _currentCadency;

    /// <summary>
    /// <para>Instantiates a pool of bullets</para>
    /// <para>Assigns a delegate to the OnShoot event of the Controller</para>
    /// </summary>
    private void Awake()
    {
        _objectPooler = new ObjectPooler();
        _objectPooler.InstantiateObjects(10, bullet, "Bullets");
        controller.OnShoot += Shoot;
    }

    /// <summary>
    /// <para>Gets a GameObject from the ObjectPooler and position it in front of the current GameObject</para>
    /// <para>If _canShoot is false then it will not spawn any bullet</para>
    /// </summary>
    private void Shoot()
    {
        if (!_canShoot) return;
        
        var nextBullet = _objectPooler.GetNextObject();

        nextBullet.gameObject.layer = gameObject.layer;
        var bulletTransform = nextBullet.transform;
        var myTransform = transform;
        var bulletPosition = myTransform.position;
        
        bulletTransform.rotation = myTransform.rotation;
        bulletPosition += myTransform.up;
        bulletTransform.position = bulletPosition;

        _canShoot = false;
    }

    /// <summary>
    /// <para>Calculates how much time has passed after the last shoot. If it passed more time than the cadence
    /// then _canShoot will be true.</para>
    /// </summary>
    private void Update()
    {
        if (_canShoot) return;
        _currentCadency += Time.deltaTime;
        if (_currentCadency < cadence) return;
        
        _canShoot = true;
        _currentCadency = 0;
        if(nonStop) Shoot();
    }
}