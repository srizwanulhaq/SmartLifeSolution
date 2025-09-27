using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dto.Profile
{

    public class ProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ProfileDto2
    {
        public string type { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public string url { get; set; }
        public ProfileBody body { get; set; }
    }

    public class ProfileBody
    {
        public string name { get; set; }
    }

}
