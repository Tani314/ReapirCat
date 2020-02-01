using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Experience
{
    public class ColliderReceiverController : MonoBehaviour
    {
        public UnityEvent OnHighlight;
        public UnityEvent OnSelect;
        public UnityEvent OnLeave;
    }
}
