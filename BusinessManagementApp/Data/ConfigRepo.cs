﻿using BusinessManagementApp.Data.Model;

namespace BusinessManagementApp.Data
{
    public class ConfigRepo
    {
        public Config Config { get; set; } = new();

        public void LoadConfig()
        {
            Config = new Config()
            {
                MinOvertimeHour = 17,
                MaxOvertimeHour = 20,
                OvertimeHourlyRate = 150_000
            };
        }

        public void SaveConfig()
        {

        }
    }
}