﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTRS
{
    class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValue(string KeyParam, string ValueParam)
        {
            Key = KeyParam;
            Value = ValueParam;
        }
    }
}
