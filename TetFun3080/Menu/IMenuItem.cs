﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080.Menu
{
    internal interface IMenuItem:IEntity
    {
        abstract void OnSetSelected(bool selected);
        abstract void OnInteracted();
    }
}
