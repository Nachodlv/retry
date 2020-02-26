using UnityEngine;

/// <summary>
/// <para>Shoots bullets when the controller event is called</para>
/// </summary>
[RequireComponent(typeof(Controllable))]
public class Shooter : MonoBehaviour
{
    [SerializeField] [Tooltip("Bullet that will be shoot")]
    private Pooleable bullet;

    [SerializeField] [Tooltip("Time in seconds between each shot")]
    private float cadence = 1f;

    [SerializeField] [Tooltip("If nonStop is true it will not stop shooting")]
    private bool nonStop = false;

    private Controllable controllable;
    private ObjectPooler _objectPooler;
    private bool _canShoot;
    private float _currentCadency;

    /// <summary>
    /// <para>Removes the bullets currently present in the Scene</para>
    /// </summary>
    public void RemoveBullets()
    {
        _objectPooler.DeactivatePooleables();
    }

    /// <summary>
    /// <para>Instantiates a pool of bullets</para>
    /// <para>Assigns a delegate to the OnShoot event of the Controller</para>
    /// </summary>
    private void Awake()
    {
        _objectPooler = new ObjectPooler();
        _objectPooler.InstantiateObjects(10, bullet, "Bullets");
        controllable = GetComponent<Controllable>();
        controllable.OnShoot += Shoot;
    }

    /// <summary>
    /// <para>Gets a GameObject from the ObjectPooler and position it in front of the current GameObject</para>
    /// <para>If _canShoot is false then it will not spawn any bullet</para>
    /// </summary>
    private void Shoot()
    {
        if (!_canShoot) return;
        
        var nextBullet = _objectPooler.GetNextObject();

        var bulletTransform = nextBullet.transform; // TODO generating GC
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