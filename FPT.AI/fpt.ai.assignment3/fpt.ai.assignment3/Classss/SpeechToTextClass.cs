﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fpt.ai.assignment3.Classss
{
    class SpeechToTextClass
    {
        public string status { get; set; }
        public List<DictonaryEntry> hypotheses { get; set; }
        public string id { get; set; }
    }

    class DictonaryEntry
    {
        public string utterance { get; set; }
    }
}
