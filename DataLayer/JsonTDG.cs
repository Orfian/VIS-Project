using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DataLayer
{
    public class JsonTDG
    {
        public static void Export(string json)
        {
            System.IO.File.WriteAllText("receipt.json", json);
        }
    }
}
