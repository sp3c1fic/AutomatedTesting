// <copyright file="Logger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing
{
    using System.Reflection;
    using log4net;
    using log4net.Config;

    public class Logger
    {
        private const string LogFileName = "log4net.config";

        /// <summary>
        /// Method responsible for loading up the log4net configuration.
        /// </summary>
        public static void ConfigureLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly() !);
            XmlConfigurator.Configure(logRepository, new FileInfo(LogFileName));
        }
    }
}
