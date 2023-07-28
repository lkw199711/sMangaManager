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
        public static string downloadRoute= @"C:\Program-User\10temp\06\download\";
        public static string cookie = "language=tw; viewModeSaving=1; userKeyToken=15d27a24e1a22987d24aee4c99c1917e_1689925853; service_key=e7cdac5be1e5cd7b6041b87bfb616665; _gcl_au=1.1.2074426609.1689925857; __lt__cid=75a9432a-e104-4e09-8a16-dbab6d5b9064; _ga=GA1.1.1319261843.1689925859; adultStatus=1; notOpenConfirmAdult=1; saveId=lkw199711%40163.com; freeTimeGiveInfo=%7B%22LD3%22%3A1689926119%7D; isLoginUsed=1; net_session=elFmR3g5UWRsYm1ZZlIrOHlNNk4wRWdHTTd5b1dDeHA0MDRVblNXK0VmV1N1RjFhWXJOZStqUlIxcnU5bTA5VUZkVDgvS1lwUlNFdldKM3JBTkFvZlNWUEkzQkU2cUxPbndmcVFuUFB2QmV5bGJXRnlMOHlpcjNtOUMyWDZaZWRCOWVvbmZOdXMzUkpDQ3V5MERnbHhvYmhORmEyVmVJZ0F3NEtGbVN4SkMzbFRLVWU0Q2d1VVB2aEs3bXprNy9MYy9sRE42VU9Wd2FJRzR6UHExZUozUnR5WDhVeCtETlJ1U1NvMG53OFZneEVwSmNuRUxydlQ2K3FoU09XbTNtdTZ3T1gvYTRYUnFHdTBMTXprMGUxbW9GWFk5RlVEaUtNRFhPNDRTTlFEc2g4VmoraGJlazBtV3JwM3BEZEFsREFHdFN4VXlhMGtzcnZOR284cW4rdVJnbGI0b1Fabi9kL2V1Rk12N3JmNGUxNTlmRHpEREQ0QVhwcm9vdVlrVzFCMktwTXV3S0t5SlRtYzBqdlpCZGpGZzZJclFvcmQ0SnlqLzBJVjhEa0pZbEJYNzlYQkxnYk9PUWhSZldzMXpiM3p5aHFSMzRjamVCOHFEazliYWtsQ3g5N3V4L0wvVEhYS3VQclJ5eFViQVpaZkFJZGlLOTl1SW1CeTA4alBBamI3Tmw4VjhOZzZteWtqa0JROWgwdUdWdnc5VGVGakxlWWNWOHhKMFdaYVZQYjJVR05PZG5MQ1B6eTZaV2NZeHlGf0118085649aad90f5226e57852d563f581163bc; userGender=1; notOpenGiftNotice=1; existsBannerTop=1; userKey=6BC96BA0A3A0CD6122EC2C2319EEBB921D8A4763360662553ECACCED9DF9C108; comicEpListSort=asc; userRecentTop10ListToLS=1; viewNonstop=1; notOpenPhoneCertNotice=1; notOpenAttendancePopup=1; hotTimeGiveInfo=%7B%22hotTimeSet%22%3A1689933579%7D; notOpenPopupNotice=1; __lt__sid=c93d9a72-7fc17385; epListAccessPath=%2Fsearch; spush_nowUrl=https%253A%252F%252Fwww.toptoon.net%252Fcomic%252FepList%252F80643; _ga_CZ8J0XSEDJ=GS1.1.1689933471.3.1.1689933617.31.0.0";
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
