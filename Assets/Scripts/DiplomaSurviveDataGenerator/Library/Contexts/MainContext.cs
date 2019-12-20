﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaSurviveDataGenerator
{
    public class MainContext : Context, IMainContext
    {
        protected int? _level;
        public virtual int? Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnLevelChanged?.Invoke();
            }
        }

        public event ValueChanged OnLevelChanged;

        public MainContext()
        {
            OnLevelChanged += ContextChanged;
        }
    }
}
