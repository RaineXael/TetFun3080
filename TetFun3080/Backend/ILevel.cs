using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    internal interface ILevel :IEntity
    {
        public void OnEnter();
        public void OnExit();
        public void OnPause();

        public abstract List<IEntity> entities { get; set; }


    }
}
