using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

public class ObjectPooler
{
    private List<Pooleable> objects;
    private int initialQuantity;
    private GameObject parent;
    private Pooleable objectToPool;
    private Action<List<Pooleable>> onGrow;

    public List<Pooleable> Objects => objects;

    /// <summary>
    /// Instantiates game objects with the given <paramref name="pooleable"/> and <paramref name="quantity"/>.
    /// The new gameObjects will have their parent called <paramref name="parentName"/>
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="pooleable"></param>
    /// <param name="parentName"></param>
    /// <param name="grow">Method that will be executed when the pool grows.</param>
    public void InstantiateObjects(int quantity, Pooleable pooleable, string parentName,
        Action<List<Pooleable>> grow = null)
    {
        initialQuantity = quantity;
        parent = new GameObject
        {
            name = parentName
        };
        objectToPool = pooleable;
        objects = new List<Pooleable>();
        onGrow = grow;
        Grow();
    }

    /// <summary>
    /// <para>Returns the next available Pooleable. If there is no Pooleable deactivated then it will
    /// instantiate more</para>
    /// </summary>
    /// <returns></returns>
    public Pooleable GetNextObject()
    {
        while (true)
        {
            foreach (var pooleable in objects)
            {
                if (pooleable.IsActive) continue;
                
                pooleable.Activate();
                return pooleable;
            }
            Grow(); 
        }
    }

    private void Grow()
    {
        var quantity = objects.Count;
        for (var i = 0; i < initialQuantity; i++)
        {
            var newObject = Object.Instantiate(objectToPool, parent.transform, true);
            newObject.gameObject.SetActive(false);
            objects.Add(newObject);
        }
        onGrow?.Invoke(objects.GetRange(quantity, initialQuantity));
    }

    /// <summary>
    /// <para>Deactivates all the Pooleables</para>
    /// </summary>
    public void DeactivatePooleables()
    {
        foreach (var pooleable in objects)
        {
            pooleable.Deactivate();
        }
    }
}