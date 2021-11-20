public class TimeUtility
{
    public static string TimeFormat(int m_totalSecond)
    {
        int hour, minute, second;
        hour = m_totalSecond / 60 / 60;
        m_totalSecond = m_totalSecond % (60 * 60);
        minute = m_totalSecond / 60;
        m_totalSecond = m_totalSecond % 60;
        second = m_totalSecond;
        string res = "";
        if (hour <= 9) res += "0" + hour;
        else res += hour;
        res += ":";
        if (minute <= 9) res += "0" + minute;
        else res += minute;
        res += ":";
        if (second <= 9) res += "0" + second;
        else res += second;
        return res;
    }
}