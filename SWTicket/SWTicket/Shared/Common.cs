using System;

namespace SWTicket.Shared
{
    public class Common
    {        
        public static decimal ToDecimalConvertObject(object obj, decimal alterValue)
        {
            decimal result = 0;
            try
            {
                if (obj != null)
                {
                    if (obj.ToString() != string.Empty)
                    {
                        result = Convert.ToDecimal(obj.ToString());
                    }
                    else
                    {
                        result = alterValue;
                    }
                }
                else
                {
                    result = alterValue;
                }
            }
            catch
            {
                result = alterValue;
            }
            return result;
        }

        public static int ToIntConvertObject(object obj, int alterValue)
        {
            int result = 0;
            try
            {
                if (obj != null)
                {
                    if (obj.ToString() != string.Empty)
                    {
                        result = Convert.ToInt32(obj.ToString());
                    }
                    else
                    {
                        result = alterValue;
                    }
                }
                else
                {
                    result = alterValue;
                }
            }
            catch
            {
                result = alterValue;
            }
            return result;
        }

        public static DateTime ToDateTimeConvertObjectElseToday(object obj)
        {
            DateTime result = DateTime.Now.Date;
            try
            {
                if (obj != null)
                {
                    if (obj.ToString() != string.Empty)
                    {
                        result = Convert.ToDateTime(obj.ToString());
                    }
                    else
                    {
                        result = DateTime.Now.Date;
                    }
                }
                else
                {
                    result = DateTime.Now.Date;
                }
            }
            catch
            {
                result = DateTime.Now.Date;
            }
            return result;
        }


    }
}
