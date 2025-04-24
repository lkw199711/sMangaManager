using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sMangaManager.FormIndex;

namespace globalData
{
    
    class global
    {
        public static string website;
        public static string downloadRoute= @"A:\02manga\download\";
        public static string reanmneRoute = @"A:\02manga\download\rename\";
        public static string cookie = "_uetvid=a7022a50307b11ee8d72b92e2785d84a; language=tw; __lt__cid=75a9432a-e104-4e09-8a16-dbab6d5b9064; adultStatus=1; saveId=lkw199711%40163.com; notOpenConfirmAdult=1; isLoginUsed=1; userGender=1; _tt_enable_cookie=1; _ttp=GjtkO1MrEZs633wEcccMDRWR_mu; _clck=srkw94%7C2%7Cfjr%7C0%7C1481; cto_bundle=5_QnIV9qVVg5Q2xadGtTWUxsNiUyQlZialU0TWxkQW0wMU45d1NqWmFEQzBPUXVNc005VVhFUG5CMWpTRVlHQWlGayUyRjhqUWFxMGklMkJrUTY0dlpDYiUyQjhMSjF6VkhQazdFdzEwJTJCaGJFTTdna0xNWGtKc3ZBcHd4WlBVQ3BkbWdIbHcxOEthUDIwcVV4Q0lMRFd0NGhYbnVyTnR6N2p3JTNEJTNE; isUseForm=e; _ga=GA1.1.363943355.1705835059; _ga_CZ8J0XSEDJ=GS1.1.1728814968.39.0.1728816668.60.0.0; viewModeSaving=1; userKeyToken=7db3a7f2e2d3ff1199f2bb79882496bc_1739342447; service_key=a0738b517b471c138d9b44b94a2d90d6; comicEpListSort=asc; freeTimeGiveInfo=%7B%22LD4%22%3A1739345929%7D; userKey=6BC96BA0A3A0CD6122EC2C2319EEBB921D8A4763360662553ECACCED9DF9C108; timeUTC=0; existsBannerTop=%23ffeea9; epListAccessPath=%2Fcomic%2FepList%2F81107; spush_nowUrl=https%253A%252F%252Fwww.toptoon.net%252Fcomic%252FepList%252F81107; notOpenGoJoinAlert=1; userGradeDownInfo=1; bannerToLS=1; hotTimeGiveInfo=%7B%22hotTimeSet%22%3A1740383827%7D; checkSetGiftbox=1; userGiftboxToLS=1; userGiftPackageListToLS=1; net_session=w%2F6jmGDOjVJkxMkOS8JROQKuDZ8AOItkFoBKsPKxSsDwM%2BN%2Bs4BfmoJv1l0zDZCNdGf14sitcNog7ck7Bj077TxmsIN0ApIMp8y3YwlArybExaX%2Bm%2FoPKl%2BWx92LhmxhP9xX4FPb4NTsHDERst%2F%2FZYceyqE961yzUTldhXRX7x0IO%2B9rvvg28eUYnEgpni6ZpE65qJxxyHn8mBwaj63i3k72rjb4MJzLfqJs7UrwxSxh7AUHRxtvF0jcnz%2BhRKEXyByls7lBhKKFKcThGnttccq%2F1ztJ%2BFBbnVM8PonCH9XZywZB4e6wr9ZZM1Q48Y52ybqmXjEEYla5Zh0G26GH5lErdfR%2B1PjmgRYtX8gu1%2BmvEy5OQwKqwoFTBg%2Fm1ET3Ait5h7hXBLCKmYCq3pSC6pU%2BpqZ5fpPQJPBd5JWWTyNHz4XqeR5RItNERPc5tNEExb58Gcqg0UqsqXsb8vvyWXp6G7kqQpUH%2FaP2IejN2B8LFMfA5E5RfyHV29p%2FT2pUQm4URXb4QP6HOMvkaGf6XCduFYxS9MvK6k1VEIC7kcysqlYY7YdAK9VAaefxytzh%2FYj%2FnieV575ZODpOJmuYjz29T0jfwRXFfRDsW%2BZO3JL1aR7g13lbinVYwbeYSGOtb62c1d89fe5864d4c97f26a196780431997e9bbb; userCoinInfo=%7B%22userCoinWeb%22%3A%220%22%2C%22userCoinSilver%22%3A%220%22%2C%22userCoinBronze%22%3A%220%22%2C%22userCoinCoupon%22%3A0%2C%22userCoinBronzeDDay%22%3A0%2C%22userCoin%22%3A0%7D; userPayCount=0; userLastActionDt=2025-02-24%2015%3A57%3A07; userKeyTokenToLS=1; userSubscriptionListToLS=1; recentAdd=1676%7C57459; userRecentTop10ListToLS=1";

        public static BindingList<string> routers = new BindingList<string>();
        public static Config config = new Config();
        public static string url = "";
        public static string html = "";
        public static bool alreadyAlertImageProblem = false;
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
