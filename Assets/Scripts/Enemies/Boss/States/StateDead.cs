using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Scripts.Enemies.Boss
{
    public class StateDead : State
    {
        public StateDead(StateMachineBoss boss) : base(boss) { }

        public override void OnEnter()
        {
            MachineBoss.IsDead = true;
            MachineBoss.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            MachineBoss.Animator.SetBool("IsDead", MachineBoss.IsDead);
            MachineBoss.GetComponentsInChildren<Rigidbody2D>().ToList().ForEach(x => x.constraints = RigidbodyConstraints2D.FreezeAll);

            var win = FindInActiveObjectsByTag("Winner");

            if (win != null)
                win[0].SetActive(true);
            else
                Debug.LogError("pas de win");

            var timer = GameObject.Find("TIMER");
            if (timer != null)
                timer.GetComponent<Timer>().timerIsRunning = false;
            else
                Debug.LogError("pas de timer");

            Time.timeScale = 0f;


            GameObject.Destroy(MachineBoss.gameObject, 3f);

        }

        GameObject[] FindInActiveObjectsByTag(string tag)
        {
            List<GameObject> validTransforms = new List<GameObject>();
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].gameObject.CompareTag(tag))
                    {
                        validTransforms.Add(objs[i].gameObject);
                    }
                }
            }
            return validTransforms.ToArray();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnTriggerEnter()
        {

        }
    }
}
