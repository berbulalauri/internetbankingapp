namespace BBS.Web.Constants
{
    public class AddCronJob
    {
        public const string CronExpressionForDevelopment = @"* * * * *";
        public const string CronExpressionForDeployment = @"0 18 * * *";
        public const string CurrentEnvironmentName = "ASPNETCORE_ENVIRONMENT";
        public const string DevelopmentEnvironment = "Development";
    }
}
