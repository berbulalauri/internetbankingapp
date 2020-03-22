using BBS.Web.Constants;
using System;

namespace BBS.Web.Helpers
{
    public class ConvertIntToCronExpression
    {

        public static string GetCronExpression()
        {
            if (Environment.GetEnvironmentVariable(AddCronJob.CurrentEnvironmentName) == AddCronJob.DevelopmentEnvironment)
            {
                return AddCronJob.CronExpressionForDevelopment;
            }
            else
            {
                return AddCronJob.CronExpressionForDeployment;
            }
        }
    }
}
