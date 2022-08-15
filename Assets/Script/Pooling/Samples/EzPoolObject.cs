using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pooling
{
    public class EzPoolObject : PoolObject<EzPoolObject>
    {

        protected override void OnRented()
        {
            base.OnRented();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
