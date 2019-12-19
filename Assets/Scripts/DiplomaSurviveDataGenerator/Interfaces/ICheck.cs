﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public interface ICheck
    {
        DeductionType DeductionType { get; set; }
        ICheckStep CheckChain { set; }

        double Check();
        void NeedCheck();
    }
}