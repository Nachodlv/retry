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
        this.objectToPool = pooleable;
        objects = new List<Pooleable>();
        onGrow = grow;
        Grow();
    }

    /// <summary>
    /// Returns the next Pooleable deactivated.
    /// If the are no more Pooleables deactivated then it will instantiate more.
    /// </summary>
    /// <returns></returns>
    public Pooleable GetNextObject()
    {
        if (objects.Count == 0) Grow();

        var first = objects.First();
        objects.RemoveAt(0);
        first.Activate(this);
        return first;
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
    /// Adds the Pooleable to the objects lists meaning that the Pooleable is ready to be used again.
    /// </summary>
    /// <remarks>This method is executed by the Pooleable itself when is no more needed</remarks>
    /// <param name="pooleable">The Pooleable that was deactivated</param>
    public void PooleableDeactivated(Pooleable pooleable)
    {
        objects.Add(pooleable);
    }
}