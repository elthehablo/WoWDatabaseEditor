﻿using System;
using System.Linq;
using AsyncAwaitBestPractices.MVVM;
using WDE.Common;
using WDE.Common.Managers;
using WDE.Common.Services;
using WDE.Common.Utils;
using WDE.Module.Attributes;
using WoWDatabaseEditorCore.Services.ConfigurationService.ViewModels;

namespace WoWDatabaseEditorCore.Services.ConfigurationService
{
    [AutoRegister]
    [SingleInstance]
    public class ConfigureService : IConfigureService
    {
        private readonly IDocumentManager documentManager;
        private readonly Func<ConfigurationPanelViewModel> settings;

        private ConfigurationPanelViewModel? openedPanel = null;

        public ConfigureService(IDocumentManager documentManager, Func<ConfigurationPanelViewModel> settings)
        {
            this.documentManager = documentManager;
            this.settings = settings;
        }

        private void ShowSettings(Type? panelToOpen)
        {
            if (openedPanel == null)
            {
                openedPanel = settings();
                IAsyncCommand? origCommand = openedPanel.CloseCommand;
                openedPanel.CloseCommand = new AsyncCommand(async () =>
                {
                    if (origCommand != null)
                        await origCommand.ExecuteAsync();
                    openedPanel = null;
                });
            }

            if (panelToOpen != null &&
                openedPanel.ContainerTabItems.FirstOrDefault(x => x.GetType() == panelToOpen) is { } configurable)
                openedPanel.SelectedTabItem = configurable;
            
            documentManager.OpenDocument(openedPanel);
        }

        public void ShowSettings() => ShowSettings(null);
        
        public void ShowSettings<T>() where T : IConfigurable => ShowSettings(typeof(T));
    }
}