using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portali.Models
{
    public class Njoftim
    {
        public string ID { get; set; }
        public string Titulli { get; set; }
        public string Permbajtja { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string SSN_P { get; set; }

        public string IdKursi { get; set; }
        public string Emer_Kursi { get; set; }
        public string Emer_Pedagogu { get; set; }
        public string Mbiemer_Pedagogu { get; set; }
        public Njoftim(string id, string titulli, string permbajtja, string data, string status, string ssn_p, string id_kursi, string emer_kursi, string emer_pedagogu, string mbiemer_pedagogu)
        {
            ID = id;
            Titulli = titulli;
            Permbajtja = permbajtja;
            Data = data;
            Status = status;
            SSN_P = ssn_p;
            IdKursi = id_kursi;
            Emer_Kursi = emer_kursi;
            Emer_Pedagogu = emer_pedagogu;
            Mbiemer_Pedagogu = mbiemer_pedagogu;
        }
        public Njoftim() { }
    }
}