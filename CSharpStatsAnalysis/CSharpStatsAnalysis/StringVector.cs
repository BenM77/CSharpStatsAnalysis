﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    class StringVector: Vector<string>
    {
        public StringVector(string[] arr)
        {
            vecArr = arr;
        }
    }
}