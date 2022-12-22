﻿using BusinessManagementApp.Data.Model;

namespace BusinessManagementApp.Data
{
    public class ConfigRepo
    {
        public Config Config { get; set; } = new();

        public ConfigRepo()
        {
            // TODO: Remove when using real API
            LoadConfig();
        }

        public void LoadConfig()
        {
            Config = new Config()
            {
                MaxNumOfOvertimeHours = 4,
                OvertimeHourlyRate = 150_000
            };
        }

        public void SaveConfig()
        {
        }
    }
}