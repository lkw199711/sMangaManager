using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    
    class global
    {
        public static string website;
        public static string downloadRoute= @"D:\8other\01manga\顶通\download\";
        public static string cookie = "language=tw; viewModeSaving=1; userKeyToken=3972044f4cb5678ed7b06a6b880cfb6c_1694724112; service_key=39ac4f93cc82bf729d28acd3b9203f5b; __lt__cid=849f08ba-21b0-4ea4-8cd9-769d6b7b771d; _gcl_au=1.1.165631631.1694724114; adultStatus=1; comicEpListSort=asc; saveId=lkw199711%40163.com; notOpenConfirmAdult=1; freeTimeGiveInfo=%7B%22LD3%22%3A1694737778%7D; isLoginUsed=1; userGender=1; cto_bundle=ksjRdl9EUFo1akNHRmZHUVRacXVBRHJpbXh2Z3hac2NPYWJJY1IlMkJzV3MlMkJhbklZRVlwaU91OW82S0hEVVlLSnYlMkJYdXQ4a0xDelpUTTAxSkJJdEpaUFJMNWtrbkU2STFGWnZyU1g2SERDUVlsWlNRNm1rMWFKbkdZblJ5TnZGcWJzcTVSNEZzSk5hc0FBZ1c4VUttNXFabVp6ZlElM0QlM0Q; _ga_CZ8J0XSEDJ=deleted; _uetvid=13ea4e40e2c111edbe55e7bb792d7ea9; net_session=elFmR3g5UWRsYm1ZZlIrOHlNNk4wRkg0VHV2TUhydjZYUVhhS0FGanU2cXpzRlhYdlByRmlQbDJBdHl0Tm9sKzFoNURIeE5ZMGdKZTVHdWZOWks5ejFDeVM5RmNJVnVDNnNHaXpHdU5VUi81aU1qNW85VEJXSkhmT1hueUtsVFFWZ2FJMmFPaXFzcVAyLzMvb01taGlhVlBiTGNaNFU3cU9MOGF5WUxyOGdFNnJuLzhENlJTbWxGSFlxclNGQ1BPOTlCTVlWVzBNNFVBbHU2UjBiUWsrVzNUd2RvK0ZPQ3ZRcFUzTCtMbHhyWE8xd1hrTExuTXlZRVZnNlZ0OGI5UncvS0pYOTZQdGhBZUUxVWw1bHhoaTJtelpDcGdCeGRnTWJTTGJCMkJzNm1jR2Zudk5FbjJlRXdFdy9pVC9Ic2IyNVJRRkNES29NMVFzckwzalpZVnVTbjlkZHpseTA1VjRnSW5SRHQvb0cwVUhMZ21qNkhBWW9lZkorM3huZFBRdEZ4M2x6YyszbW1kQ3hiMHZMZ1laUTkzRDJrMG1FdkdkelZ3Q1JTbkZQN25xNUcxTGZRQ0hFSjc1Y0Q4Z1RLNEkxZC9OdE5ZUGkwbnFWcVg0TnZzMG45N2NHeGRWTkR6Y3ExQTFQTEVtMTUzbWx1amRKK1RaaVl6ZmJIblRyelRBcjdVTlNNSGllRHJ2TjFVSVF3NjE3WVgwNUlqVFZuSGF6dlY2OHo5bUVZSHBENnlZRW5Kbmt0ZEtMV0lUeWRMdb91abb9b34541a85f9e078d63480033358f615f; hotTimeGiveInfo=%7B%22hotTimeSet%22%3A1696416930%7D; userKey=6BC96BA0A3A0CD6122EC2C2319EEBB921D8A4763360662553ECACCED9DF9C108; _gid=GA1.2.1472554962.1696416933; _im_vid=01HBX58AQDY9QVBPGM6F9WMAV9; _clck=x7zws6|2|ffk|0|1364; userRecentTop10ListToLS=1; viewNonstop=1; notOpenPhoneCertNotice=1; notOpenAttendancePopup=1; existsBannerTop=%2388e6d9; _ga=GA1.1.526383988.1694724114; _uetsid=89a0b56062a411ee8943117d60f117bc; _ga_CZ8J0XSEDJ=GS1.1.1696416932.15.1.1696417057.60.0.0; userGiftboxCount=%7B%22tw%22%3A4%7D; _clsk=8yjvew|1696421124987|1|1|e.clarity.ms/collect";
    }

    class utils
    {
        public static string format_file_name(string str)
        {
            return str.Replace("\n", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("_", "");
        }
    }
}
