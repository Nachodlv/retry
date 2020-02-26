using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// <para>Runs multiple coroutines one after the other</para>
    /// </summary>
    public class CoroutineQueue
    {
        private Queue<IEnumerator> coroutines;
        private bool executingCoroutines;
        private MonoBehaviour monoBehaviour;

        public CoroutineQueue(MonoBehaviour monoBehaviour, int initialQuantity)
        {
            this.monoBehaviour = monoBehaviour;
            coroutines = new Queue<IEnumerator>(initialQuantity);
        }

        /// <summary>
        /// <para>Adds a new coroutine to the queue. If no coroutine is executing, it will start executing the
        /// coroutine passed as parameter</para>
        /// </summary>
        /// <param name="coroutine"></param>
        public void AddCoroutine(IEnumerator coroutine)
        {
            coroutines.Enqueue(coroutine);
            if(executingCoroutines) return;
            executingCoroutines = true;
            monoBehaviour.StartCoroutine(ExecuteCoroutines());
        }

        private IEnumerator ExecuteCoroutines()
        {
            while (coroutines.Count > 0)
            {
                yield return monoBehaviour.StartCoroutine(coroutines.Peek());
                coroutines.Dequeue();
            }

            executingCoroutines = false;
        }
    }
}