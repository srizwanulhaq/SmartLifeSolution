using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Helpers
{
    public static class Extensions
    {
        public static string FromHexToBase64(this string str)
        {
            try
            {
                byte[] bytes = Convert.FromHexString(str);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
