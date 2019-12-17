﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handlers
{
    public interface IPersonalLifeHandler
    {
        event Action<int, int> PersonalLifeChanged;
        void ReducePrivateLife();

        int MaxPrivateLife { get; }

        int PrivateLife { get; }
    }

}
