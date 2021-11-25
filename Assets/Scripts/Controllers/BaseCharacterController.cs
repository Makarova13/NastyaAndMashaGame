using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    public Creature Creature { get; protected set; }
}
