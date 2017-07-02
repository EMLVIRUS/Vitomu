﻿using Digimezzo.Utilities.Utils;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using Vitomu.Base;

namespace Vitomu.Services.WindowsIntegration
{
    public class JumpListService : IJumpListService
    {
        #region Variables
        private System.Windows.Shell.JumpList jumpList;
        #endregion

        #region Construction
        public JumpListService()
        {
            this.jumpList = System.Windows.Shell.JumpList.GetJumpList(Application.Current);
        }
        #endregion

        #region IJumpListService
        public async void PopulateJumpListAsync()
        {
            await Task.Run(() =>
            {
                if (this.jumpList != null)
                {
                    this.jumpList.JumpItems.Clear();
                    this.jumpList.ShowFrequentCategory = false;
                    this.jumpList.ShowRecentCategory = false;

                    this.jumpList.JumpItems.Add(new JumpTask
                    {
                        Title = ResourceUtils.GetStringResource("Language_Donate"),
                        Arguments = "/donate " + ContactInformation.PayPalLink,
                        Description = "",
                        IconResourcePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Defaults.IconsLibrary),
                        ApplicationPath = Assembly.GetEntryAssembly().Location,
                        IconResourceIndex = 0
                    });
                }

            });

            if (this.jumpList != null) this.jumpList.Apply();
        }
        #endregion
    }
}
