using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

using System.Security.Cryptography;

using System.Net;
using System.Net.Mail;

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;

namespace Winner.Extends
{
    public static class ExtentionsClass
    {
        public static void MsgBox(this Controller controller, string msg, string key = "msgbox")
        {
            controller.ViewData[key] = msg;
        }

        public static string SessionKey(this ISession session)
        {
            return "awei";
        }
        public static string SessionAdminKey(this ISession session)
        {
            return "ishare";
        }
        /// <summary>
        /// 设置session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool Set<T>(this ISession session, string key, T val)
        {
            if (string.IsNullOrWhiteSpace(key) || val == null) { return false; }

            var strVal = JsonConvert.SerializeObject(val);
            var bb = Encoding.UTF8.GetBytes(strVal);
            session.Set(key, bb);
            return true;
        }

        /// <summary>
        /// 获取session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var t = default(T);
            if (string.IsNullOrWhiteSpace(key)) { return t; }

            if (session.TryGetValue(key, out byte[] val))
            {
                var strVal = Encoding.UTF8.GetString(val);
                t = JsonConvert.DeserializeObject<T>(strVal);
            }
            return t;
        }

        #region 获取Ip

        public static string GetUserIp(this Controller controller)
        {
            return controller.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        #endregion

        #region 格式化时间

        /// <summary>
        /// 获取据当前时间的时间间隔：如1年1月1日
        /// </summary>
        /// <param name="date"></param>
        /// <param name="yearNum"></param>
        /// <param name="monthNum"></param>
        /// <returns></returns>
        public static string FormatDateToNow(this DateTime date, int yearNum = 365, int monthNum = 31)
        {
            var subTime = DateTime.Now.Subtract(date);

            var dayNum = subTime.Days;

            var year = dayNum / yearNum;
            var month = dayNum % yearNum / monthNum;
            var day = dayNum % yearNum % monthNum;

            var str = year > 0 ? $"{year}年" : "";
            str += month > 0 ? $"{month}月" : "";
            str += day > 0 ? $"{day}天" : "1天";

            return str;
        }

        #endregion

        #region 格式化省略字符

        public static string FomartPhone(this string val, int startLen = 3, int endLen = 3)
        {
            if (string.IsNullOrWhiteSpace(val)) { return ""; }

            var len = val.Trim().Length;
            var start = string.Empty;
            var end = string.Empty;
            if (len > startLen) { start = val.Substring(0, startLen); } else { start = val; }
            if (len - endLen > startLen) { end = val.Substring(len - endLen, endLen); }
            return string.Format("{0}***{1}", start, end);
        }
        #endregion;
        public static string FomartSubStr(this string val, int startLen = 20, string op = "...")
        {
            if (string.IsNullOrWhiteSpace(val)) { return ""; }
            val = val.Trim();
            if (val.Length < startLen) { return val; }
            else
            {
                val = val.Substring(0, startLen) + op;
            }
            return val;
        }
        /// <summary>
        /// 获取2个字符串之间的字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="beginTag">开始字符串</param>
        /// <param name="endTag">截止字符串</param>
        /// <returns></returns>
        public static string GetBetweenString(string str, string beginTag, string endTag)
        {
            int startIndex = str.IndexOf(beginTag);
            int endIndex = str.LastIndexOf(endTag);
            int length = endIndex - (startIndex + 1);

            return length == -1 ? "" : str.Substring(startIndex + 1, length);
        }
        /// <summary>
        /// 获取2个字符串之间的字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="beginTag">开始字符串</param>
        /// <param name="endTag">截止字符串</param>
        /// <param name="beginOffset">开始字符串偏移量</param>
        /// <returns></returns>
        public static string GetBetweenString(string str, string beginTag, string endTag, int beginOffset)
        {
            int startIndex = str.IndexOf(beginTag);
            int endIndex = str.LastIndexOf(endTag);
            int length = endIndex - (startIndex);

            return length == -1 ? "" : str.Substring(startIndex + beginOffset, length - beginOffset);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();
        }

        /// <summary>
        /// 旧文件拷贝到新地址中
        /// </summary>
        /// <param name="oldpath">旧文件的服务器路径(包括文件名)</param>
        /// <param name="newpath">新文件的服务器路径(包括文件名)</param>
        /// <param name="over">如果已经存在相应文件是否覆盖</param>
        /// <returns></returns>
        public static bool CopyFileTo(string oldpath, string newpath, bool over)
        {
            bool suss = false;
            if (System.IO.File.Exists(oldpath))
            {
                try
                {
                    System.IO.File.Copy(oldpath, newpath, over);
                    suss = true;
                }
                catch
                {
                    suss = false;
                }
            }
            return suss;
        }
        public static int StringToInt(string str, int valdefault)
        {
            int val = valdefault;
            try
            {
                val = int.Parse(str);
            }
            catch
            {

            }
            return val;
        }
        public static float StringToFloat(string str, float valdefault)
        {
            float val = valdefault;
            try
            {
                val = float.Parse(str);
            }
            catch
            {

            }
            return val;
        }
        public static decimal StringToDecimal(string str, decimal valdefault)
        {
            decimal val = valdefault;
            try
            {
                val = decimal.Parse(str);
            }
            catch
            {

            }
            return val;
        }
        public static double StringToDouble(string str, double valdefault)
        {
            double val = valdefault;
            try
            {
                val = double.Parse(str);
            }
            catch
            {

            }
            return val;
        }
        public static bool StringToBool(string str, bool defaultval)
        {
            bool val = defaultval;
            try
            {
                if (str == "true" || str == "True")
                {
                    val = true;
                }
                else if (str == "false" || str == "False")
                {
                    val = false;
                }
                else
                {
                    val = defaultval;
                }
            }
            catch
            {
                val = defaultval;
            }
            return val;
        }
        /// <summary>
        /// 把输入字符串转换成半角模式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToDBC(string str)
        {
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
            }
            return new string(c);
        }
        /// <summary>
        /// 把输入字符串转换为全角模式（中文）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToSBC(string str)
        {
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                {
                    if (c[i] >= 65 && c[i] <= 90)
                    {
                        c[i] = (char)c[i];
                    }
                    else
                    {
                        c[i] = (char)(c[i] + 65248);
                    }
                }
            }
            return new string(c);
        }
        /// <summary>
        /// 获得单个字符的首字母（如单独的汉字，或者单独的一个字母，非字符串）
        /// </summary>
        /// <param name="onlycode"></param>
        /// <returns></returns>
        public static string GetOnlyIndexCode(string onlycode)
        {
            if (Convert.ToChar(onlycode) >= 0 && Convert.ToChar(onlycode) < 256)
            {
                return onlycode;
            }
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(onlycode);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160) + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)));
            }
        }
        /// <summary>
        /// 根据区位获得首字母
        /// </summary>
        /// <param name="GBCode"></param>
        /// <returns></returns>
        public static string GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }
        public static DateTime StringToDateTime(string str, DateTime time)
        {
            DateTime result = time;
            bool resval = DateTime.TryParse(str, out result);
            if (!resval)
            {
                result = time;
            }
            return result;
        }

        /// <summary>
        /// 获取指定字符串内从指定索引开始的固定长度的字符串
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <param name="startindex">开始索引位置</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetStrBeginIndexLength(string str, int startindex, int length)
        {
            if (str.Length > startindex)
            {
                str = str.Substring(startindex, length - 1);
            }
            else
            {
                str = "";
            }
            return str;
        }
        /// <summary>
        /// 删除字符串的html格式
        /// </summary>
        /// <param name="htmlstring"></param>
        /// <returns></returns>
        public static string DelHtmlTag(string htmlstring)
        {
            //删除脚本
            htmlstring = htmlstring.Replace("\r\n", "");
            htmlstring = Regex.Replace(htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, "\\(\\d{4}-\\d{2}-\\d{2}\\)", "", RegexOptions.IgnoreCase);
            htmlstring = htmlstring.Replace("<", "");
            htmlstring = htmlstring.Replace(">", "");
            htmlstring = htmlstring.Replace("\r\n", "");
            htmlstring = htmlstring.Replace("&ldquo;", "“");
            htmlstring = htmlstring.Replace("&rdquo;", "”");
            htmlstring = WebUtility.HtmlEncode(htmlstring).Trim();
            return htmlstring;
        }
        /// <summary>  
        /// 该方法用于生成指定位数的随机数字+字符 
        /// </summary>  
        /// <param name="VcodeNum">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字加字符符串</returns>  
        public static string RndNumStr(int VcodeNum)
        {
            //验证码可以显示的字符集合  
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                ",R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
            string code = "";//产生的随机数  
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                int t = rand.Next(61);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RndNumStr(VcodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += VcArray[t];//随机数的位数加一  
            }
            return code;
        }
        /// <summary>  
        /// 该方法用于生成指定位数的随机数字
        /// </summary>  
        /// <param name="VcodeNum">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        public static string RndNum(int VcodeNum)
        {
            int number;
            char code;
            string smscode = string.Empty;

            System.Random random = new Random();

            for (int i = 0; i < VcodeNum; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));

                smscode += code.ToString();
            }
            return smscode;
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Body">邮件内容</param>
        /// <param name="To">发送到的邮箱</param>
        /// <param name="WhereFrom">使用发送的邮箱账户</param>
        /// <param name="WherePwd">使用发送的邮箱密码</param>
        /// <param name="FromName">显示的来源名称</param>
        /// <param name="Smtp">指定的SMTP邮件服务器</param>
        /// <returns>返回真为发送成功，假为发送失败</returns>
        public static bool SendMail(string Subject, string Body, string To, string WhereFrom, string WherePwd, string FromName, string Smtp)
        {
            bool resval = true;
            try
            {
                MailMessage msg = new MailMessage();
                //msg.From = new MailAddress(WhereFrom, FromName);
                msg.From = new MailAddress(WhereFrom);
                msg.To.Add(new MailAddress(To));

                msg.Subject = Subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient(Smtp, 465);//之前之所以发布出去是由于阿里云屏蔽25端口，改为SSL模式发送用465端口
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(WhereFrom.Split('@')[0], WherePwd);   //帐号密码 
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                client.Send(msg);
                msg.Dispose();
                resval = true;
            }
            catch
            {
                resval = false;
            }
            return resval;
        }
        public static SendSmsResponse SendSms(string mobile, string alikeyid, string alikeysecret, string alisignname, string templatecode, string templateparam)
        {
            //产品名称:云通信短信API产品,开发者无需替换
            const String product = "Dysmsapi";
            //产品域名,开发者无需替换
            const String domain = "dysmsapi.aliyuncs.com";

            // TODO 此处需要替换成开发者自己的AK(在阿里云访问控制台寻找)
            string accessKeyId = alikeyid;
            string accessKeySecret = alikeysecret;
            //以下为发送短信验证码
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            SendSmsResponse response = null;
            try
            {

                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = mobile;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = alisignname;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = templatecode;
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = templateparam;
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                //request.OutId = msgid.ToString();
                //请求失败这里会抛ClientException异常
                response = acsClient.GetAcsResponse(request);

            }
            catch (ServerException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            catch (ClientException e)
            {
                Console.WriteLine(e.ErrorCode);
            }
            return response;
        }
        public static string SendSmsNew(string mobile, string alikeyid, string alikeysecret, string alisignname, string templatecode, string templateparam)
        {
            // *** 需用户填写部分 ***

            //fixme 必填: 请参阅 https://ak-console.aliyun.com/ 取得您的AK信息

            string accessKeyId = alikeyid;//你的accessKeyId，参考本文档步骤2
            string accessKeySecret = alikeysecret;//你的accessKeySecret，参考本文档步骤2

            Dictionary<string, string> smsDict = new Dictionary<string, string>();

            //fixme 必填: 短信接收号码
            smsDict.Add("PhoneNumbers", mobile);

            //fixme 必填: 短信签名，应严格按"签名名称"填写，请参考: https://dysms.console.aliyun.com/dysms.htm#/develop/sign
            smsDict.Add("SignName", alisignname);

            //fixme 必填: 短信模板Code，应严格按"模板CODE"填写, 请参考: https://dysms.console.aliyun.com/dysms.htm#/develop/template

            smsDict.Add("TemplateCode", templatecode);

            // fixme 可选: 设置模板参数, 假如模板中存在变量需要替换则为必填项
            smsDict.Add("TemplateParam", templateparam);

            //什么？Newtonsoft.Json也觉得重，那拼字符串好了
            //smsDict.Add("TemplateParam", "{\"appname\":\"微关爱\",\"appstorename\":\"小黑\"}");



            // *** 以下内容无需修改 ***
            smsDict.Add("RegionId", "cn-hangzhou");
            smsDict.Add("Action", "SendSms");
            smsDict.Add("Version", "2017-05-25");

            string domain = "dysmsapi.aliyuncs.com";//短信API产品域名（接口地址固定，无需修改）

            // 初始化SignatureHelper实例用于设置参数，签名以及发送请求
            var singnature = new SignatureHelper();

            return singnature.Request(accessKeyId, accessKeySecret, domain, smsDict);
        }
        public static string CreateOrderNum()
        {
            string prefix = "Ai";
            string suffix = "";
            //随机生成一个数字
            System.Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                int number = rand.Next(1, 9);
                suffix += number;
            }


            //根据时间和随机数字生成订单编号
            string ordernumber = prefix + DateTime.Now.ToString("yyyymmddhhmmssfff") + suffix;

            return ordernumber;
        }
        public static string CreateReturnNum()
        {
            string prefix = "R";
            string suffix = "";
            //随机生成一个数字
            System.Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                int number = rand.Next(1, 9);
                suffix += number;
            }


            //根据时间和随机数字生成订单编号
            string ordernumber = prefix + DateTime.Now.ToString("yyyymmddhhmmssfff") + suffix;

            return ordernumber;
        }
    }
}
