﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace saonGroup.UI.Models
{
    public class ProvinceModel
    {
        [DisplayName("COUNTRY")]
        public string name { get; set; }


        [DisplayName("ISO")]
        public string iso { get; set; }

        [DisplayName("CASES")] 
        public int confirmed { get; set; }

        [DisplayName("RECOVERED")]
        public int recovered { get; set; }

        [DisplayName("DEATHS")]
        public int deaths { get; set; }

        [DisplayName("PROVINCE")]
        public string province { get; set; }
    }
}
